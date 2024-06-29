import json

from django.http import JsonResponse
from django.shortcuts import render

from History.models import History

from Serializers.HistorySerializer import HistorySerializers
from User.models import User


# Create your views here.
def getSingleHistory(request, id):
    HistoryData = History.objects.get(id=id)
    hs = HistorySerializers(HistoryData)
    return JsonResponse(hs.data, safe=False)


def insertHistory(request, userid):
    user = User.objects.get(id=userid)
    postBody = request.body
    json_result = json.loads(postBody)
    content = json_result["content"]
    matchTime = json_result["matchTime"]
    matchType = json_result["matchType"]
    isWin = json_result["isWin"]
    History.objects.create(user_id=user, content=content, matchType=matchType, matchTime=matchTime, isWin=isWin)
    return JsonResponse({"isSuccess": "success"}, safe=False, status=200)


def getHistories(request, userid):
    user = User.objects.filter(id=userid)
    Historys = user[0].history_set.filter()
    jsonData = [{
        "id":item.id,
        "content": item.content,
        "matchType": item.matchType,
        "matchTime": item.matchTime,
        "isWin": item.isWin

    } for item in Historys]
    return JsonResponse(jsonData, safe=False)
