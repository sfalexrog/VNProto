INCLUDE GameData.ink

-> start

=== start ===
# Location room_main

Утром ты первым делом пишешь Стасу. 

Я: Привет! Что у тебя стряслось? 
Я: Ау... Ты в сети? # П

Так и не получив ответа, ты отправляешься в школу.

# Location bus_parking

В ожидании автобуса снова пытаешься достучаться до Стаса.

Алексей: О! Какая встреча! # Р
Я: А?! # Effect actor_shake
Алексей: Привет, говорю. 
Алексей: Да не пугайся ты так. 
Я: Я и не пугаюсь. Просто так неожиданно...
Алексей: Ну прости, я не хотел. 
Алексей: Впредь буду за сто метров махать красным флажком. # Р
Алексей: Те ребята к тебе больше не пристают? 
Я: Нет. Всё нормально.
Алексей: В Лабораторию наведываешься? 
Алексей: Чего-то я тебя там не вижу. 

- Я: Ну...
*    Я: ...я вас там тоже не {isBoy(): видел| видела}.
     Я: А разве мы должны были встретиться? 
     Алексей: Нет, конечно. К тому же там целый лабиринт.
     Алексей: Немудрено и заблудиться! # Р 
     ->hunter

*    Я: ...я не хожу в Лабораторию.
     Алексей: А почему? 
     Я: А зачем?
     Алексей: Странно... # У
     ->hunter

*    Я: ...я вообще-то вас не знаю.
     Я: Мы виделись всего один раз. 
     Алексей: Зато какой! # Р
     Алексей: Я же тебя спас!
     Алексей: Это ли не повод пообщаться? 
     ->hunter

==hunter==
Я: Вы что-то хотели? 
Алексей: Да нет, ничего. Просто поболтать.
Алексей: Кстати, я уже рассказывал про Защитника? 

В этот момент тебе приходит СМС. 

Мама: Мы с папой после обеда уходим в гости. 
Мама: Придём поздно.  

Алексей: Что-то важное? 

Ты задумчиво смотришь на сообщение.
Значит, придётся опять сидеть с сестрёнкой. 

Алексей: Эй! А у тебя там вирус! # Effect actor_shake
Я: Где? 
Алексей: Да в телефоне же! 
Алексей: Дай сюда. Сейчас покажу. 

Алексей выхватывает смартфон у тебя из рук.

Я: Эй! Отдайте телефон! # З

Ты пытаешься забрать телефон обратно. 
Но Алексей ловко уворачивается от тебя. 

Алексей: Подожди, я же тебе помогаю. 
Алексей: Эээ... А пароль какой? 

- Я: (Я могу...)
*    [(...отвлечь его, сказав неверный пароль.)]
     Я: У меня простой. 
     Я: Три единицы и девятка. 
     Алексей: Э… Не подходит # Р
     Я: Конечно не подходит! 
     Я: Я не {isBoy(): дурак| дура}, чтобы свои пароли всем говорить!

     ->password

*    [(...спросить зачем ему мой пароль.)]
     Я: А зачем вам мой пароль?
     Алексей: Говорю же — у тебя там вирус. # З
     Алексей: Мне нужно войти в систему, чтобы показать. # З
     ->password

*    [(...не говорить пароль.)]
     Я: Вы же сотрудник Лаборатории. 
     Алексей: И что? # З
     Я: В Лаборатории меня учили никому не говорить свои пароли. 
     Алексей: Мне — можно. # З
     ->password

==password==

В этот момент ты слышишь визг тормозов. 
Поднимаешь глаза и видишь открытую дверь жёлтой машины. 

Я: О, мне пора! # Р
Я: За мной приехали. # Р

Ты выхватываешь телефон из рук Алексея и прыгаешь в машину. 

# Location bcar

Бамблби: Что это за тип? 
Я: Он мне помог спрятаться от хулиганов. 
Бамблби: Ты ему доверяешь?
Я: Даже не знаю.
Бамблби: Тогда зачем {isBoy(): дал| дала} ему телефон? 

- Я: (Я могу сказать, что...) 
*    [(...он сам отобрал телефон.) @good] 
     Я: Я не {isBoy(): отдавал| отдавала}. # П
     Я: Он выхватил телефон у меня из рук.
     Я: Ты очень вовремя приехал. 
     Я: Спасибо. 
     Бамблби: Будь осторожнее, пожалуйста. 
     ~ ChangeRelPoints(Bumblebee_RelPoints, 2)
     ->why_phone

*    [(...не хочу выслушивать нотации.) @bad] 
     Я: Да {isBoy(): понял| поняла} я всё уже! 
     Я: Ещё и гигантские роботы мне будут морали читать... 
     Бамблби: Может мне и приезжать не стоило?
     ~ ChangeRelPoints(Bumblebee_RelPoints, -2)
     Я: Ой, прости... # П
     Я: Нет, конечно...
     Бамблби: Нет — не стоило приезжать? 
     Я: Да нет же! Нет, это я про себя.
     Бамблби: И как вы люди друг друга понимаете?
     Я: С трудом...
     ->why_phone

*    [(...ничего страшного не случилось.)]
     Я: Он, конечно, странный.
     Я: Но ведь ничего не случилось.
     Бамблби: А могло случиться. 
     Я: И что же? 
     Бамблби: Он мог бы забрать твой телефон, например.
     Я: Об этом я не {isBoy(): подумал| подумала}... # П
     ->why_phone

==why_phone
Бамблби: Просто запомни: никому не давай телефон. 
Бамблби: Никому не говори пароль.
Бамблби: И следи, чтобы никто не заглянул тебе через плечо. 
Я: Хорошо. Я буду внимательнее. 
Я: Кстати, тебе показать новую фотку?
Я: Я вчера {isBoy(): был| была} на месте падения метеорита.
Бамблби: Если честно, там не метеорит упал. 
Я: А что? 
Бамблби: Там вообще ничего не падало. 
Бамблби: Это приземлился корабль десептиконов.
Я: Вот это круто!
Бамблби: Ага, и очень опасно. 
Бамблби: Тебе лучше туда не ходить. 
Бамблби: Ты же помнишь, что они охотятся за тобой?
Я: Да-да, конечно.
Я: Кстати, ты так и не рассказал мне.
Бамблби: О чём?
Я: Зачем десептиконам нужен Защитник. 
Бамблби: Как тебе объяснить...
Бамблби: О! Мы уже приехали.
Бамблби: Не опоздай в школу! 

# Location school_yard

Ты выходишь из машины и идёшь к школе. 
И почему Бамблби не хочет рассказать тебе в чём дело? 
Тут ты видишь Стаса. 
Он стоит за углом школы и угрюмо смотрит себе под ноги. 

- Я: (Я могу...)
*    [(...просто подойти к Стасу.)]
     Я: Привет, Стас! # Р
     Стас: О... Это ты... # П
     Стас: Привет... # П
     Я: Что-то случилось? 
     ->stas_joke

*    [(...крикнуть издалека, позвать Стаса.) @bad] 
     Ты машешь рукой и громко кричишь. 
     
     Я: Привет, Стас! 
     Я: Чего ты там стоишь? Идём в школу! 
     
     Стас выходит идёт к тебе, сжимая кулаки. 
     
     Стас: Зачем?! # З
     Стас: Чего ты кричишь на всю школу?! # З
     ~ ChangeRelPoints(Stas_RelPoints, -2)
     Я: Что-то не так? 
     ->stas_joke

*    [(...подойти, не привлекая внимания.) @good] 
     Ты подходишь к Стасу так, чтобы никто этого не видел. 
     
     Я: Привет. 
     Я: Вижу, у тебя что-то случилось.
     Я: Раз ты тут прячешься.
     Стас: Спасибо, что не {isBoy(): выдал| выдала} меня. 
     Стас: Да, привет... # П
     ~ ChangeRelPoints(Stas_RelPoints, 2)
     ->stas_joke

==stas_joke==
Стас: Я вчера натворил дел... # П
Я: Надеюсь, никого не убил? 
Стас: Не знаю, что лучше...
Стас: Мне вчера {isBoy(): написал Денис| написала Настя}.
Стас: Типа собирает фотографии класса. 
Я: Зачем? # У
Стас: На особый альбом класса. 
Стас: Там нужны фотки типа мы уже взрослые. 
Я: Как это? # У
Стас: Ну, с сигаретой там, с пивом. 
Я: А ты что? 
Стас: Стащил у отца сигарету и сфоткался. 
Я: Пока ничего страшного ты не рассказал. 
Стас: Так {isBoy(): он| она} теперь эту фотку грозит моему отцу выслать! # З
Я: Вот оно что... 
Я: Никакого альбома на самом деле не было.
Стас: Да... Развели меня... # П
Стас: Отец меня убьёт... # П
Я: Но ведь ты не куришь.
Стас: Он слушать не станет! # З
Стас: Увидит фотку и всё! Мне хана... # П

- Я: Стас,...
*    Я: ...тебе нужно с кем-нибудь посоветоваться. 
      Стас: А с кем? 
      Я: Например, с Пандой. 
      Стас: Да что он знает... # П
      Я: Мне кажется, он очень современный учитель. 
      Я: И рассудительный. 
     ->stas_advice

*    Я: ...давай я поговорю с {isBoy(): Денисом| Настей}? @good
     Стас: Ой... А ты не боишься? 
     Я: А чего бояться?
     Стас: Я боюсь... 
     Стас: Слушай, я тебе благодарен...
     ~ ChangeRelPoints(Stas_RelPoints, 2)
     Стас: Но я не хочу, чтобы ты тоже {isBoy(): пострадал| пострадала}.
     Я: Не волнуйся за меня. 
     Я: А ты расскажи всё Панде. 
     Я: Думаю, он подскажет, что делать. 
     ->stas_advice

*    Я: ...тебе нужно рассказать всё отцу. @bad
     Стас: Ни за что!
     Я: Почему ты боишься? 
     Я: Ты же ни в чём не виноват. 
     Стас: Ты не знаешь моего отца! 
     Стас: Нет! 
     Я: Ладно, тогда давай с Пандой посоветуемся. 
     ~ ChangeRelPoints(Stas_RelPoints, -2)
     ->stas_advice

==stas_advice==
Стас: Ладно... {isBoy(): Уговорил| Уговорила}...

# Location classroom

После уроков вы вдвоём подходите к учителю. 

Панда: Есть вопросы по уроку, ребята? 
Я: Нет. Можно у вас спросить совета?
Панда: Конечно. 
Я: Только это очень личный вопрос.
Панда: Обещаю, что разговор останется между нами. 
Я: Стас, расскажи. 

Стас мнётся, но пересказывает свою проблему. 
Панда задумчиво поправляет очки. 

Панда: Да уж... Ситуация у вас, Станислав, неприятная. 
Панда: В интернете же вся информация хранится, считай, вечно.
Панда: Её могут раскопать и через десятки лет. 
Я: Простите? А как это к Стасу относится? 
Панда: Это я к тому, что в интернете вообще нужно быть осторожным. 
Панда: Не писать того, за что потом может быть стыдно.
Панда: Не показывать фотографий, которые нельзя видеть всем.
Стас: А я и не показывал всем... 
Панда: Передать фотографию другому, значит, показать всем. 
Панда: Это сейчас, Станислав, вам грозит лишь гнев отца. 
Панда: А если бы я выкладывал в вашем возрасте подобное...
Панда: Это сейчас бы нашли и меня уволили. 
Я: Да. Мы понимаем, что в интернете нужно следить за языком. 
Я: Но Стасу-то что делать? 
Панда: Увы, единственный способ разоружить шантажистов...
Панда: ...рассказать отцу первым. 
Стас: Нет! Это невозможно... 
Панда: Станислав, будьте мужчиной.
Панда: Это лучшее решение. 

Стас расстроенный уходит из класса. 

# Location school_yard

Ты его догоняешь во дворе школы. 

Стас: Я к отцу не пойду!

-> stas_father
==stas_father==
- Я: (Я могу...)
*    [(...попробовать уговорить Стаса поговорить с отцом.)]
     Я: Стас... Разговор с отцом — единственный выход.
     Стас: Я же сказал — нет! # З
     Стас: Ты не знаешь моего отца! # З
     Стас: Хочешь помочь — не предлагай мне этого! # З
     -> stas_father

*    [(...сказать, что поговорю с {isBoy(): Денисом| Настей}.) ]
     Я: Тогда остаётся только одно. 
     Я: Уговорить Дениса отстать от тебя. 
     Я: Жди здесь. 
     -> stas_leader

*    [(...предложить ничего не делать.)]
     Я: {isBoy(): Денис мог| Настя могла} это затеять только чтобы тебя позлить.
     Я: На самом деле фотографию {isBoy(): он| она} отправлять не будет.
     Стас: А если будет?! # З
     Стас: Что тогда? # З
     -> stas_father

==stas_leader==

Ты подходишь к группе одноклассников. 

Лидер: Зря ты вчера {isBoy(): ушёл| ушла}. 
Лидер: Было классно. 
ЛидерПара: Сделали гигабайт крутых селфи! 
Я: {isBoy(): Денис| Настя}, можно с тобой поговорить? 
Я: Один на один. 
Лидер: Легко. 

Вы отходите от остальных. 

Лидер: Чего надо? 
Я: Отстань от Стаса. 
Лидер: Да я вроде Свина сегодня не {isBoy(): трогал| трогала}.
Я: Я про его фотку с сигаретой. 
Лидер: А! Уже наплакался тебе? 
Лидер: Нефиг! Пусть помучается. 
Я: У него будут большие проблемы. 
Я: Удали фотку. 

{
- Leader_RelPoints > 10: //нужно поборать правильное значение
        Лидер: Ладно, так и быть. 
        Лидер: Удалю, но только из уважения к тебе. 
        Я: Спасибо. 
        Лидер: Кстати, поставь себе «Вирусные гонки» на телефон.
        Я: Зачем? 
        Лидер: Сразимся как-нибудь в онлайне. 
        Я: Она же дорогая. # У
        Лидер: У меня пиратка стоит. 
        Я: Хорошо, поищу. 
        Лидер: Увидимся в игре! # Р
        Лидер: В этот раз я тебя раскатаю! # Р

- Leader_RelPoints < 10: //нужно поборать правильное значение
        Лидер: Могу, конечно. 
        Лидер: Но я хочу взять реванш. 
        Лидер: За тот проигрыш в игровом зале. 
        Я: Пожалуйста. Когда играем? 
        Лидер: Нет. Не так. 
        Лидер: Установи себе «Вирусные гонки» на телефон. 
        Лидер: Сразимся в онлайне!  
        Я: Она же кучу денег стоит! # З
        Лидер: Тогда качай пиратку. 
        Лидер: Хочешь помочь своему дорогому Свину?
        Я: Я {isBoy(): согласен| согласна}.
        Лидер: Молодец. 
        ~ ChangeRelPoints(Leader_RelPoints, 2)
        Лидер: Свин помилован. Можешь его обрадовать. 
}

Ты подходишь к Стасу.

Стас: Ну как? 
Я: Можешь спать спокойно. 
Я: {isBoy(): Он обещал| Она обещала} удалить фотографию. 
Стас: Ура!!! # Р
Стас: Спасибо тебе огромное! # Р
~ ChangeRelPoints(Stas_RelPoints, 2)

Стас чуть не бросается тебя обнимать. 
Ты вовремя его останавливаешь и идёшь на автобусную остановку. 

# Location bus_parking

В ожидании автобуса ищешь игру. 
Через несколько минут находишь подходящий сайт. 
В этот раз обходится без отправки СМС. 
Ты запускаешь игру и пишешь сообщение {isBoy(): Денису| Насте}.

Я: Это @playername@.
Я: Я игре. 
Лидер: Супер. 
Лидер: Вечером — бой!

Внезапно экран перекрывает рекламное сообщение. 

# Effect screen_shake

Ты закрываешь его, но через пару секунд оно возникает снова.
Нужно звонить в Лабораторию. 
Тебе удаётся позвонить лишь через несколько минут мучений. 

Мэй: Привет!
Я: Мэй, у меня тут что-то с телефоном...
Мэй: И почему я не удивлена? Рассказывай. 

- Я: А что рассказывать...
*    Я: ...я ничего не {isBoy(): делал| делала}. @bad
     Я: Вдруг на телефоне появилась реклама.
     Я: И она не убирается. 
     Мэй: Когда же ты поймёшь, что мне не нужно врать? # П
     ~ ChangeRelPoints(Lab_RelPoints, -2)
     Я: Я не вру!
     Мэй: Телефон — это кучка пластмассы и микросхем. 
     Мэй: В нём само по себе ничего не делается. 
     Мэй: Признавайся, что ты {isBoy(): сделал| сделала}?
     ->virus

*    Я: ...похоже я {isBoy(): полез| полезла} куда не следует. @good
     Мэй: Умение признавать свои ошибки — признак умных людей. 
     ~ ChangeRelPoints(Lab_RelPoints, 2)
     Мэй: И что именно ты {isBoy(): сделал| сделала}?
     ->virus

*    Я: ...на телефон влезла реклама.
     Мэй: Сама влезла? 
     Я: Ну... Наверное, я что-то {isBoy(): сделал| сделала}. # П
     Мэй: А что именно? 
     ->virus

==virus==
Я: {isBoy(): Скачал| Скачала} игру. Пиратскую. 
Мэй: Так я и знала. 
Мэй: Что ж, поздравляю — у тебя вирус. 
Я: И как быть? 
Мэй: Лечить, конечно же. 
Мэй: Сейчас я тебе пришлю ссылку. 
Мэй: Скачаешь и установишь. Это наш программа-сканер.
Мэй: Она вычистит твой телефон. 
Я: Спасибо! 
Мэй: Будь осторожнее с пиратскими программами. 

После установки программы Мэй, реклама исчезает. 
Сразу же приходит сообщение от {isBoy(): Вики| Вити}.

Симпатия: А ты молодец. 
Я: Ты о чём?
Симпатия: Я знаю, что ты {isBoy(): помог| помогла} Стасу. 
Я: Так должен был поступить любой.
Симпатия: И многие из класса так поступили? 
Симпатия: Вижу, у нас появился действительно хороший человек. 
~ ChangeRelPoints(Love_RelPoints, 2)
Симпатия: Домой едешь? 
Я: Нет. В спортклуб. 
Я: Нужно записаться в {isBoy(): футбольную команду| команду по спортивным танцам}. 
Симпатия: Удачи тебе. 
Я: А она мне понадобится? 
Симпатия: На сколько я знаю тренера, да. 

Если раньше ты не {isBoy(): сомневался| сомневалась}, что попадёшь в команду.
То после слов {isBoy(): Вики| Вити} тебя охватывает волнение. 
->END