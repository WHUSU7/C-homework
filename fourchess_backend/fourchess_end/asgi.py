"""
ASGI config for fourchess_end project.

It exposes the ASGI callable as a module-level variable named ``application``.

For more information on this file, see
https://docs.djangoproject.com/en/4.2/howto/deployment/asgi/
uvicorn fourchess_end.asgi:application --host 0.0.0.0 --port 8000

"""

import os
from django.core.asgi import get_asgi_application
from channels.routing import ProtocolTypeRouter, URLRouter

from . import routing

os.environ.setdefault('DJANGO_SETTINGS_MODULE', 'fourchess_end.settings')

application = ProtocolTypeRouter({
    "http": get_asgi_application(),
    "websocket": URLRouter(routing.websocket_urlpatterns),

})
