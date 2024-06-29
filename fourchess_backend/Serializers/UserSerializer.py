from rest_framework import serializers

from User.models import User


class UserBasicSerializers(serializers.ModelSerializer):
    class Meta:
        model = User
        fields = ['name', 'password']


class UserSerializer(serializers.ModelSerializer):
    class Meta:
        model = User
        fields = '__all__'
