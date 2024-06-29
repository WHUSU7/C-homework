import queue

from django.core.exceptions import ObjectDoesNotExist
from django.http import JsonResponse, HttpResponse
from django.shortcuts import render
import json

from Serializers.UserSerializer import UserSerializer
from User.models import User

from django.core.cache import cache

user_Queue = {}


# Create your views here.
def login(request):
    postBody = request.body
    json_result = json.loads(postBody)
    name = json_result['name']
    password = json_result['password']
    try:
        user = User.objects.get(name=name, password=password);
        userSerializer = UserSerializer(user)
        return JsonResponse(userSerializer.data, safe=False, status=200)
    except ObjectDoesNotExist:
        return HttpResponse(status=401)


def register(request):
    postBody = request.body
    json_result = json.loads(postBody)
    name = json_result['name']
    password = json_result['password']
    nickname = json_result['nickname']
    isUserExist = User.objects.filter(name=name)
    if (isUserExist.exists()):
        wrongUser = {
            "id": -1,
            "name": isUserExist[0].name,
            "password": isUserExist[0].password,
            "nickname": isUserExist[0].nickname
        }
        userSerializer = UserSerializer(wrongUser)
        return JsonResponse(userSerializer.data, safe=False, status=400)
    else:
        User.objects.create(name=name, password=password, nickname=nickname)
        user = User.objects.get(name=name, password=password, nickname=nickname);
        userSerializer = UserSerializer(user)
        return JsonResponse(userSerializer.data, safe=False, status=200)


# 长轮询
def createPvp(request, userid):
    user_Queue[userid] = queue.Queue()
    if (cache.get('Flag')):
        cache.set('Flag', False)
        return JsonResponse(1, safe=False, status=200)
    else:
        cache.set('Flag', True)
        return JsonResponse(-1, safe=False, status=200)


def clientSendMsg(request, userid):
    postBody = request.body
    json_result = json.loads(postBody)
    msg = json_result['msg']
    turn = json_result['turn']

    jsonData = {
        "msg": msg,
        "turn": turn
    }
    for userid, q in user_Queue.items():
        q.put(jsonData)

    return JsonResponse({"isSuccess": "success"}, safe=False, status=200)


def clientGetMsg(request, userid):
    q = user_Queue[userid]
    try:
        data = q.get(timeout=10)

        return JsonResponse(data, status=200, safe=False)
    except queue.Empty as e:
        return JsonResponse({"isSuccess": "failed"}, status=400, safe=False)


def index(request):
    return render(request, 'index.html')

def modifyUserName(request, userid):
    postBody = request.body
    json_result = json.loads(postBody)
    newName = json_result['nickname']
    User.objects.filter(id=userid).update(nickname=newName)
    return JsonResponse({"isSuccess": "success"}, safe=False)
def updateProfilePicture(request, userid):
    postBody = request.body
    json_result = json.loads(postBody)
    newProfile = json_result['profilePicture']
    User.objects.filter(id=userid).update(profilePicture=newProfile)

    return JsonResponse({"isSuccess": "success"}, safe=False)


def getProfilePicture(request, userid):
    user = User.objects.get(id=userid)
    nowProfile = user.profilePicture

    return JsonResponse({"profilePicture": nowProfile}, safe=False)
