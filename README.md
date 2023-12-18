# WeatherProject
Best Practice
В данном проекте так же добавил EF Core. Бд и кафку поднимал в докере. 
Апи токен погоды у меня хранится в системных переменных.
Для генерации доки и тестирования метода использовал swagger.

# UPD проблема решена. теперь для работы из коробки вам надо указать апи токен и поднять два докер контейнера.
![image](https://github.com/Cheasy101/WeatherProject/assets/70900183/21fa2895-e23e-4ddb-9aaf-3db09759cd8f) 
 вместо переменной среды можете использовать свой апи токен, полученный на accuweather


## Далее ваше вниманию представляю инструкцию по скачиванию и настройке проекта.
# Инструкция по установке для VisualStudio
Скачиваем проект с помощью gitBash
![image](https://github.com/Cheasy101/WeatherProject/assets/70900183/4880e6dd-4a66-47fc-b839-fbf3f9547b2d)
Далее разворачиваем проект в VisualStudio и открываем sln файл.
Далее нам потребуется поднять два докер контейнера. 
1 хранится в компоузе в WorkerService A 
он содержит зуу кипер и кафку
далее я не разобрался как в вижуал студио поднять compose файлы. Я работаю в райдере и он делает это автоматический. Так что я опишу создание докер контейнеров в конце файла

Далее создаем запуск сразу всех 3 микросервисов. Делается это в этой вкладке.
![image](https://github.com/Cheasy101/WeatherProject/assets/70900183/a04b393a-4586-4e92-a2aa-1661270c9040)
Указываем все 3 проекта. ( WorkerServiceAClient содержит только 1 файл с дто шаблоном поэтому его указывать не нужно)
![image](https://github.com/Cheasy101/WeatherProject/assets/70900183/e5e681aa-ffa3-4256-8a4a-75c280503b2b)
Далее запускаем проект и при первой сборке у нас появляются вот такие ошибки. 
 
![image](https://github.com/Cheasy101/WeatherProject/assets/70900183/93d5c0da-becc-4cf2-9423-30c30c943035)
Связанно это с тем, что сервисы не видят ссылку на шаблонный DTO file, который лежит в WorkerServiceAClient. Чтобы это исправить надо сделать одно действие. 
В дирректории сервиса B перейдите в папку dependencies
![image](https://github.com/Cheasy101/WeatherProject/assets/70900183/7da0ba37-0749-498c-8f6b-3e4ffbd671e7)
Добавьте ссылку на этот проект 
![image](https://github.com/Cheasy101/WeatherProject/assets/70900183/103440e4-3dfc-49c5-9284-4132f75169c4)
После данного преобразования ошибки должны устраниться. 
Проверяем….
![image](https://github.com/Cheasy101/WeatherProject/assets/70900183/a7c2bd39-9d5a-4af8-8fba-ac1a9dc96a9a)
Так же хочу добавить, что токен, по которому я получаю данные из AccuWeatherApi – я храню в системных переменных. Вам надо использовать свой токен. 
Подключение по токену хранится в сервисе А в файле program
![image](https://github.com/Cheasy101/WeatherProject/assets/70900183/270ea453-d5c3-4148-8273-73dfeca925d7)
В моем случае он имеет название ACCU_WEATHER_API_KEY.
Все работает! 
проверяем бд
![image](https://github.com/Cheasy101/WeatherProject/assets/70900183/f0f78475-79f2-42b2-972b-793888302812)
Действительно запись от 15:57 была добавлена в бд ! 
И так.
чтобы поднять докер контейнеры с помощью консольных команд нам надо использовать следующие шаги:
1.	В системной консоли перейдите в папку вашего проекта. (по умолчанию она называется weatherProject)
![image](https://github.com/Cheasy101/WeatherProject/assets/70900183/e2e2a660-eed0-4d50-b8f7-e9096b106149)
3.	Т.к компоуз файла два и они содержатся в сервисе А и в сервисе С. Вам надо будет перейти в две эти директории по очереди и выполнить там следующие команды.
4.	С помощью команды CD перейдите в папку WorkerServiceA
5.	Выполните команду (docker-compose up)
6.	Далее если вам надо удалить или остановить команду используйте команду (docker-compose down).
7.	А вообще я рекомендую пользоваться официальным интерфейсом докер. С его помощью можно отслеживать работу контейнеров
8.	![image](https://github.com/Cheasy101/WeatherProject/assets/70900183/6e440994-15bd-414b-bea0-dcd8a47c2726)


## Теперь настройки для райдера. Первичные шаги аналогичны.
Скачиваем проект, открываем его через райдер.

![image](https://github.com/Cheasy101/WeatherProject/assets/70900183/b63a7c82-b2a1-4e70-91c3-358deda519fd)
Как вы помните нам необходимы два докер контейнера. Райдер автоматический распознает компоуз файлы.
![image](https://github.com/Cheasy101/WeatherProject/assets/70900183/145decc8-b094-43ad-970d-cade4641e0c0)
Поэтому советую их создать таким способом. Просто нажать на пуск
Далее райдер сразу видит ошибку, связанную с тем, что утеряна ссылка на DTO класс в ClientServiceA

![image](https://github.com/Cheasy101/WeatherProject/assets/70900183/3d6c1d06-3543-4b70-8935-cce2de5948ef)
По депенденсис правой кнопкой, далее выбираем reference и из выбранного списка выбирает вот эту зависимость
![image](https://github.com/Cheasy101/WeatherProject/assets/70900183/5603e8a7-befd-43d1-95c7-7cb790f943db)
Нажимаем кнопку ок ! 
Далее не забываем про токен ! 
После этого создаем так называем Multiply Launch
![image](https://github.com/Cheasy101/WeatherProject/assets/70900183/a7daf296-6175-4ae4-bc4f-1d5e833458c8)
Через плюсик добавляем Compound конфигурацию. Она позволит нам добавить несколько сервисов в 1 общую конфигурацию, которую и будет запускать.
![image](https://github.com/Cheasy101/WeatherProject/assets/70900183/0ce14724-658b-456d-a035-d5281304b767)

Добавили, переименовали на Multiply Launch, нажали ОК 
 ![image](https://github.com/Cheasy101/WeatherProject/assets/70900183/90a6b0f6-562c-45d2-87ca-cb43a844356a)
после. С правильным токеном и работающими контейнерами запускаем приложение.
![image](https://github.com/Cheasy101/WeatherProject/assets/70900183/053a87a2-a871-4065-812d-75cb5bdc2c52)
видим, что все ок.
Далее в райдере можно подключиться к бд. 
в сервисе С в компоуз файле указана вся конфигурация.
Справа в IDE видим возможность подключиться к бд.
Далее выполняем эти действия
![image](https://github.com/Cheasy101/WeatherProject/assets/70900183/5af6fc09-7a3c-4cc1-a719-82cf3582921e)
Нажимаем на коннект
![image](https://github.com/Cheasy101/WeatherProject/assets/70900183/810184c3-869e-450c-8442-971795aca98b)
выбираем первый пункт, идем дальше 
![image](https://github.com/Cheasy101/WeatherProject/assets/70900183/94f88b60-f474-46dd-a43e-66daf4816b67)

Не забываем поменять порт. 
Я его «пробросил» вручную, т.к у меня на компьютере есть установленный постгресс, который забирает порт по умолчанию (5432).
![image](https://github.com/Cheasy101/WeatherProject/assets/70900183/ffab3811-b30e-4003-92ea-9514c07fbb39)
Вводим данные из компоуз файла. 
так-то эти данные – тайна, но поскольку проект учебный – оставляю пароли «по умолчанию».
Тестим соединение, и если все ок – подключаемся к бд.
![image](https://github.com/Cheasy101/WeatherProject/assets/70900183/393fd5fe-29f5-4914-bf3f-04dbc2bf55ed)

Открываем вевер дата
![image](https://github.com/Cheasy101/WeatherProject/assets/70900183/5e46bff2-67a6-46ff-aae6-66dd5a86e260)
И видим, что запись добавилась ! 

![image](https://github.com/Cheasy101/WeatherProject/assets/70900183/a545fda4-d5ab-4484-9a0c-83bbfc4691aa)
Вот она в бд, а вот она в консоли при запуске приложения несколько минут назад
![image](https://github.com/Cheasy101/WeatherProject/assets/70900183/a873a8a8-5075-4028-bd29-84ea0819dad1)

Всем спасибо за внимание !
