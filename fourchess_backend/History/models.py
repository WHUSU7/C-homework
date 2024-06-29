from django.db import models

from User.models import User


# Create your models here.
class History(models.Model):
    content = models.CharField(max_length=1024)
    user_id = models.ForeignKey(User, on_delete=models.CASCADE, null=True)
    matchTime = models.CharField(max_length=1024,default="2024.6.1")
    matchType = models.CharField(max_length=1024,default="人机对战")
    isWin = models.CharField(max_length=1024,default="未知")