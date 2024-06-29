import json
import queue

from channels.exceptions import StopConsumer
from channels.generic.websocket import WebsocketConsumer
from django.core.cache import cache
from asgiref.sync import async_to_sync


class ChatConsumer(WebsocketConsumer):
    def websocket_connect(self, message):
        print("start connecting")
        # 获取组id
        groupid = self.scope['query_string'].decode('utf-8').split('=')[1]

        self.accept()
        # 将用户加入组id,后续默认给组号为1的组发信息
        async_to_sync(self.channel_layer.group_add)(groupid, self.channel_name)

        if cache.get('Flag'):
            cache.set('Flag', False)
            self.send("1")
        else:
            cache.set('Flag', True)
            self.send("-1")

    def websocket_receive(self, message):
        print(message)

        async_to_sync(self.channel_layer.group_send)("1", {"type": "func", "message": message})

    def func(self, event):
        message = event['message']
        self.send(text_data=json.dumps({
            'message': message,

        }))

    def websocket_disconnect(self, message):
        async_to_sync(self.channel_layer.group_discard)("1", self.channel_name)
        raise StopConsumer()
