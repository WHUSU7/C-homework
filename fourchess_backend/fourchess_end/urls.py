"""
URL configuration for fourchess_end project.

The `urlpatterns` list routes URLs to views. For more information please see:
    https://docs.djangoproject.com/en/4.2/topics/http/urls/
Examples:
Function views
    1. Add an import:  from my_app import views
    2. Add a URL to urlpatterns:  path('', views.home, name='home')
Class-based views
    1. Add an import:  from other_app.views import Home
    2. Add a URL to urlpatterns:  path('', Home.as_view(), name='home')
Including another URLconf
    1. Import the include() function: from django.urls import include, path
    2. Add a URL to urlpatterns:  path('blog/', include('blog.urls'))
"""
from django.contrib import admin
from django.urls import path, re_path
import User.views as User
import History.views as History

urlpatterns = [
    path("admin/", admin.site.urls),
    re_path("m1/4020303-0-default/fourchess/history(\d+)", History.getSingleHistory),
    re_path("m1/4020303-0-default/fourchess/(\d+)/histories", History.getHistories),
    path("m1/4020303-0-default/fourchess/login", User.login),
    path("m1/4020303-0-default/fourchess/register", User.register),
    re_path("m1/4020303-0-default/fourchess/(\d+)/insertHistory", History.insertHistory),

    re_path("m1/4020303-0-default/fourchess/(\d+)/createPvp", User.createPvp),
    re_path("m1/4020303-0-default/fourchess/(\d+)/clientGetMsg", User.clientGetMsg),
    re_path("m1/4020303-0-default/fourchess/(\d+)/clientSendMsg", User.clientSendMsg),
    re_path("m1/4020303-0-default/fourchess/(\d+)/getProfilePicture", User.getProfilePicture),
    re_path("m1/4020303-0-default/fourchess/(\d+)/updateProfilePicture", User.updateProfilePicture),
    re_path("m1/4020303-0-default/fourchess/(\d+)/modifyUserName", User.modifyUserName),
    path("index", User.index)
]
