from rest_framework import serializers

from History.models import History


class HistorySerializers(serializers.ModelSerializer):
    class Meta:
        model = History
        fields = '__all__'
