﻿{"inkVersion":18,"root":[["\n",{"->":"start"},["done",{"#f":7,"#n":"g-0"}],null],"done",{"start":["^Location: ","ev",{"x()":"isGenderBoy"},"/ev",[{"->":".^.b","c":true},{"b":["^ Комната м ",{"->":"start.6"},null]}],[{"->":".^.b"},{"b":["^ Комната д",{"->":"start.6"},null]}],"nop","\n","ev",{"x()":"isGenderBoy"},"/ev",[{"->":".^.b","c":true},{"b":["^ Ты далеко не любитель рано просыпаться ",{"->":"start.13"},null]}],[{"->":".^.b"},{"b":["^ Ты далеко не любительница рано просыпаться",{"->":"start.13"},null]}],"nop","\n","^А просыпаться 1 сентября - тем более.","\n","^Первые десять минут ты обычно просто лежишь и тупишь в смартфон.","\n","^Это помогает проснуться.","\n","^В это утро остатки сна покинули тебя раньше обычного,","\n","^Потому что тебе пришло странное сообщение:","\n","^Location: Телефон","\n","^«Смотри скорее, там такое…»","\n","^Я: О, это еще что? ",{"#":"удивление"},"\n","^Я: Какие-то новости из старой школы? ",{"#":"удивление"},"\n","^Я: Больше похоже на какой-то прикол.","\n","^Но любопытство взяло верх...","\n","^Тебя перекинуло на страницу ВК.","\n","^Я: “Введите логин-пароль”...","\n","^Я: Зашибись! ",{"#":"злость"},"\n","^Я: Еще вчера все работало!","\n","^Я: Окей, вот тебе мой логин-пароль.","\n","^Но стоило ввести логин-пароль и нажать “ок”...","\n","^Как смартфон загадочно подмигнул тебе и перезагрузился.","\n","^Location: ","ev",{"x()":"isGenderBoy"},"/ev",[{"->":".^.b","c":true},{"b":["^ Комната М ",{"->":"start.60"},null]}],[{"->":".^.b"},{"b":["^ Комната Д",{"->":"start.60"},null]}],"nop","\n","^Я: Класс! Батарея села! ",{"#":"злость"},"\n","^Я: Самое время.","\n","^Тук-тук!","\n","^Мама: Проснулось, Красно-солнышко? ",{"#":"радость"},"\n","^Мама: Бегом одеваться! Завтрак стынет!","\n",{"->":"kitchen"},{"#f":3}],"kitchen":[["^Location: Кухня","\n","^На кухне солнечно и приятно пахнет блинами.","\n","^Ты хватаешь еще горячий блин и макаешь его в варенье.","\n","^Мама: Ну, как блины? ",{"#":"радость"},"\n","^Я: Отлично, мам. ",{"#":"радость"},"\n","^Мама всегда поддерживает тебя.","\n","^Сегодня она специально встала пораньше, чтобы приготовить твои любимые блины.","\n","^Она очень хочет быть твоим другом.","\n","^Но для тебя она все-таки больше мама.","\n","^Мама: Волнуешься в свой первый день в школе?","\n","^Ваши выборы влияют на дальнейшее развитие сюжета. Выбирайте с умом!","\n","^Я: Ну... ",{"#":"defenderAvailable: false"},"\n","ev","str","^Нет, с чего бы?","/str","/ev",{"*":".^.c-0","flg":20},"ev","str","^ Да, я боюсь облажаться…","/str","/ev",{"*":".^.c-1","flg":20},{"c-0":["\n",{"->":"Conflict01"},{"#f":7}],"c-1":["\n",{"->":"Conflict01"},{"#f":7}]}],{"#f":3}],"Conflict01":[["^Мама: Ой, брось. Волноваться - это нормально. ",{"#":"радость"},"\n","^Мама: Я три раза меняла школу. ",{"#":"радость"},"\n","^Мама: Только запомни:","\n","^Мама: Если тебя задирают - не бойся дать сдачи,","\n","^Мама: И рассказать взрослым о своих проблемах.","\n","^Я: Спасибо, мам. ",{"#":"печаль"},"\n","^Я: Ты умеешь успокоить. ",{"#":"печаль"},"\n","^Мама: А как твои друзья? Писали тебе летом?","\n","^Я: Угу. Сегодня вот один прислал…","\n","ev",{"x()":"isGenderBoy"},"/ev",[{"->":".^.b","c":true},{"b":["^ Ты вспомнил то странное сообщение!",{"->":".^.^.^.27"},null]}],[{"->":".^.b"},{"b":["^Ты вспомнила то странное сообщение!",{"->":".^.^.^.27"},null]}],"nop","\n","^Интересно, что там?","\n","^Я: Мам, спасибо! Мне пора!","\n","^Мама: Так, погоди! ",{"#":"удивление"},"\n","^Мама: Помнишь, о чем мы с тобой договаривались? ",{"#":"злость"},"\n","^Мама: Теперь ты сам моешь тарелку за собой.","\n","^Я: Ну мам... ",{"#":"печаль"},{"#":"defenderAvailable: false"},"\n","ev","str","^Только не сегодня…","/str","/ev",{"*":".^.c-0","flg":20},"ev","str","^Ладно, вымою…","/str","/ev",{"*":".^.c-1","flg":20},{"c-0":["\n",{"->":"Conflict02"},{"#f":7}],"c-1":["\n","ev",{"VAR?":"MomHelpStates"},{"VAR?":"WashDishes"},"+",{"temp=":"MomHelpStates","re":true},"/ev",{"->":"Conflict02"},{"#f":7}]}],{"#f":3}],"Conflict02":[["^Мама: И не надо на меня так смотреть!","\n","^Мама: Это не я люблю качать права, что “уже не ребенок”.","\n","^Тебе пришлось вымыть посуду.","\n","^Наверное, не случилось бы ничего плохого,","\n","^Оставь ты это до вечера...","\n","^Но уговор есть уговор.","\n","^Мама: Теперь я вижу, что у меня растет настоящий помощник! ",{"#":"радость"},"\n","^Location: ","ev",{"x()":"isGenderBoy"},"/ev",[{"->":".^.b","c":true},{"b":["^ Комната м ",{"->":".^.^.^.21"},null]}],[{"->":".^.b"},{"b":["^ Комната д",{"->":".^.^.^.21"},null]}],"nop","\n","^Смартфон уже должен был зарядиться.","\n","^Тебе не терпелось прочесть наконец сообщение.","\n","^Только твоя рука потянулась за смартфоном, как…","\n","^Мама: Глазам не верю! ",{"#":"удивление"},"\n","^Мама: Мы только три недели назад как переехали, ",{"#":"удивление"},"\n","^Мама: а у тебя опять на полу носки и бумажки разбросаны! ",{"#":"удивление"},"\n","^Я: Я все уберу вечером!","\n","^Мама: Нет, давай уж уберись сейчас. ",{"#":"злость"},"\n","^Мама: Кто из нас “уже не ребенок”?","\n","^Иногда Ваши выборы влияют на отношения с персонажами","\n","^Я: Мам... ",{"#":"defenderAvailable: false"},"\n","ev","str","^Сейчас мигом тут все уберу!","/str","/ev",{"*":".^.c-0","flg":20},"ev","str","^Я опаздываю в школу!","/str","/ev",{"*":".^.c-1","flg":20},{"c-0":["\n","ev",{"VAR?":"MomHelpStates"},{"VAR?":"CleanUp"},"+",{"temp=":"MomHelpStates","re":true},"/ev","ev",{"^var":"Mother_RelPoints","ci":-1},2,{"f()":"ChangeRelPoints"},"pop","/ev","\n","^Мама очень рада, что ты ей помогаешь","\n","^Мама: Умничка! ",{"#":"радость"},"\n","^Мама: А теперь бегом в школу!","\n",{"->":"Conflict03"},{"#f":7}],"c-1":["\n","ev",{"^var":"Mother_RelPoints","ci":-1},-2,{"f()":"ChangeRelPoints"},"pop","/ev","\n","^Маме очень не нравится, что ты опять сбегаешь, так и не сделав работу","\n","^Мама: Знаю я твои “опаздываю”. ",{"#":"злость"},"\n","^Мама: Что ж, беги тогда.","\n",{"->":"Conflict03"},{"#f":7}]}],{"#f":3}],"Conflict03":["ev",{"VAR?":"MomHelpStates"},{"list":{"MomHelpStates.WashDishes":1,"MomHelpStates.CleanUp":2}},"==","/ev",[{"->":".^.b","c":true},{"b":["\n","^Мама: заебись сынок","\n","^респет +10","\n","ev",{"^var":"Mother_RelPoints","ci":-1},10,{"f()":"ChangeRelPoints"},"pop","/ev","\n",{"->":".^.^.^.6"},null]}],"nop","\n","^Мама: И посмотри, не пришли ли квитанции за газ!","\n","^Location: Подъезд","\n","^Я: Ну наконец-то я прочитаю что там!","\n","^Я: Что за фигня? ",{"#":"удивление"},"\n","^Я: Да как так “пароль неверный”?! ",{"#":"удивление"},"\n","^Я: Не могли же у меня за полчаса угнать пароль! ",{"#":"злость"},"\n","^Раз за разом выходило одно и то же: “Неверный логин или пароль”!","\n","^Тебе еще и пришлось копаться в почтовом ящике","\n","^Похоже, почтальон путает его с мусорной корзиной.","\n","^Столько бумажек!","\n","^На пол выпала яркая красно-зеленая листовка.","\n","^Я: Хм… А это еще что? ",{"#":"удивление"},"\n","^Я: «Лаборатория Компьютерной Безопасности. У вас проблемы? Мы их решим!»","\n","ev",1,"/ev",{"temp=":"didCompleteChapter","re":true},"ev",1,"/ev",{"temp=":"expGain","re":true},"done","end",{"#f":3}],"isGenderBoy":["ev",0,"/ev","~ret",{"#f":3}],"ChangeRelPoints":[{"temp=":"value"},{"temp=":"relPoints"},"ev",{"VAR?":"relPoints"},{"VAR?":"value"},"+",{"temp=":"relPoints","re":true},"/ev",["ev",{"VAR?":"relPoints"},-15,"<","/ev",{"->":".^.b","c":true},{"b":["\n","ev",-15,"/ev",{"temp=":"relPoints","re":true},{"->":".^.^.^.10"},null]}],["ev",{"VAR?":"relPoints"},15,">","/ev",{"->":".^.b","c":true},{"b":["\n","ev",15,"/ev",{"temp=":"relPoints","re":true},{"->":".^.^.^.10"},null]}],"nop","\n",{"#f":3}],"global decl":["ev",0,{"VAR=":"Stas_RelPoints"},0,{"VAR=":"Leader_RelPoints"},0,{"VAR=":"Love_RelPoints"},0,{"VAR=":"Mother_RelPoints"},0,{"VAR=":"didCompleteChapter"},0,{"VAR=":"expGain"},{"list":{},"origins":["MomHelpStates"]},{"VAR=":"MomHelpStates"},"/ev","end",null],"#f":3}],"listDefs":{"MomHelpStates":{"WashDishes":1,"CleanUp":2}}}