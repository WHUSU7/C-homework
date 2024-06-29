2024年武汉大学软件构造基础大作业
基于WPF的四子棋对战平台 --“珞珈四连”

# 使用说明
work 在VS下运行
fourchess_backend 为数据库后端，需要在pycharm中运行

# 运行环境
- Visual Studio 2022及以上
- .NET Framework 4.7.2
- DataGrip + mySQL
- Pycharm + Django

# 运行方法
1. 用Pycharm打开fourchess_backend，将fourchess_backend/settings.py中80、81行的USER、PASSWORD更改为mySQL的账号密码。
2. 在Pycharm终端输入uvicorn fourchess_end.asgi:application --host 127.0.0.1 --port 8000，开启服务器。
3. VS打开work并运行。
