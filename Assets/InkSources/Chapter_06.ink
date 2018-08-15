INCLUDE GameData.ink

-> start

=== start ===
# Location school_yard

Вчерашнее предупреждение Бамблби не сбылось.
Стас: Ты чего-то сегодня {isBoy(): какой-то странный| какая-то странная}?
Я: Да так. {isBoy(): Ждал| Ждала} кое-чего. 
Стас: И чего же? # У
Я: Подвоха, но всё обошлось. 
Стас: Это же классно! # Р
Стас: Я всегда радуюсь, когда ничего плохого не происходит. 
Я: Может ты и прав. Ладно, до завтра. 
Стас: До завтра. 

# Location room_main

Не успеваешь ты войти в свою комнату, как туда входит отец. 
Отец: Ребёнок! Как насчёт поиграть в приставку?
Отец: Да на нашем огромном экране! # Р
Я: Отличная идея, пап! Когда играем? # Р
Отец: Когда ты купишь переходник. 
Я: Какой переходник? У нас же есть. 
Отец: Никак не могу его найти.
Отец: Видать, затерялся при переезде. 
Отец: Вот тебе деньги, сгоняй по-быстрому. 

- Я: (Я могу...)
*   [(...согласиться.) @good]
    Я: Легко! Куда идти? 
    Отец: Тут недалеко стоит торгово-развлекательный центр.
    Отец: Там наверняка есть магазин электроники. 
    Отец: Какой переходник нужен, помнишь? 
    Я: Обижаешь! Тот тоже я {isBoy(): покупал| покупала}.
    Ты с радостью выбегаешь из дома.
    ~ ChangeRelPoints(Parents_RelPoints, 2)
    -> shop

*    [(...поспорить с отцом.)]
     Я: Пап, ну почему всегда я? 
     Отец: Потому что ты {isBoy(): молодой и сильный| молодая и сильная}.
     Отец: А я старый и слабый. # Р
     Я: Ладно, ладно... Уговорил. Куда идти-то?
     Отец: Тут недалеко есть торгово-развлекательный центр. 
     Отец: Какой переходник нужен, помнишь? 
     Я: Конечно, пап. Тот тоже я {isBoy(): покупал| покупала}.
     Ты выходишь из дома и отправляешься в магазин.
     -> shop
    
*   [(...отказаться.) @bad]
    Я: Чего-то не хочется. 
    Отец: Не хочется играть или идти? 
    Я: Надоело, ты постоянно меня куда-то отправляешь. # З
    Я: Будто я курьер какой-то.
    Отец: Тебе полезно. И так сидишь целыми днями. 
    Отец: Полдня в школе, полдня за компьютером. 
    Я: Это не так! # З
    Отец: Хватит препираться! # З
    Отец: Одна нога здесь — другая там.
    ~ ChangeRelPoints(Parents_RelPoints, -2)
    -> shop

==shop==
Отец: Кстати, зайди на кухню. 
Отец: Мать тебе что-то хотела сказать. 

# Location kitchen

Я: Мам, ты что-то хотела? 
Мама: Да, у меня будет к тебе одна просьба.
Мама: Как вернёшься, я тебе расскажу. 
Я: А почему не сейчас? 
Мама: Это не срочно. Иди, не заставляй папу ждать. 
Я: Хорошо, я {isBoy(): побежал| побежала}!

# Location game_room

После покупки переходника тебе хочется прогуляться.
Ты поднимаешься в игровой зал и встречаешь одноклассников.

Лидер: Ущипните меня! Кто к нам пришёл!
ЛидерПара: {isBoy(): Заблудился| Заблудилась} что ли? 
ЛидерПара: Или ты хочешь поразить нас своими талантами?
Я: Могу попробовать. 
Приспешник: Куда тебе до нас? Лучше вали домой!
Лидер: А почему бы и нет? Дуэль! Выбирай оружие!

- Я: (Я могу...)
*    [(...согласиться на дуэль.) @good]
     Я: Легко. 
     Я: «Вирусные гонки».
     Денис: Отличный выбор. Ты труп.
     ~ ChangeRelPoints(Leader_RelPoints, 2)
     -> game_battle

*    [(...отказаться от дуэли.) @bad]
     Я: Я не хочу никаких дуэлей.
     Лидер: Всё ясно. 
     ЛидерПара: {isBoy(): Очередной трусливый лох| Очередная трусливая лохушка}.
     ~ ChangeRelPoints(Leader_RelPoints, -2) 
     Лидер: Или ты выберешь игру, или не выйдешь отсюда.
     Лидер: Ясно тебе?
     Я: Ладно. Пусть будет «Вирусные гонки». 
     Денис: Спасибо, что {isBoy(): облегчил| облегчила} мне задачу. 
     -> game_battle

==game_battle==
Денис начинает первым. 
Он играет действительно хорошо и набирает много очков.

Лидер: Твоя очередь. Или боишься? 
Я: А чего мне боятся? Я же труп. 

Ты начинаешь игру. 

- Я: (Я могу...)
*    [(...просто играть в полную силу.)]
     Ты проходишь уровень за уровнем, смотря только на экран. 
     
     Денис: Неплохо. Для первых уровней. 
     Приспешник: Их любой детсадовец пройдёт! 
     
     Не обращая внимания на подколы ты продолжаешь игру. 
     -> game

*    [(...сделать вид, что играю плохо.)]
     Приспешник: Ха! На первом же уровне {isBoy(): сдох| сдохла}! # Р
     Настя: Разбудите меня, когда всё закончится. 
     
     Ты делаешь вид, будто сейчас заплачешь. 
     Но потом начинаешь играть в полную силу.
     -> game
     
*    [(...покрасоваться перед одноклассниками.)]
     Ты лихо проходишь первый уровень. 
     
     Приспешник: Вот это скорость... # У
     Настя: Я даже рассмотреть не успела.
     Я: Смотри и учись! 
     
     Но в самом начале второго уровня ты теряешь жизнь. 
     
     Денис: Чему учиться-то, лошара? 
     
     Ты понимаешь, что {isBoy(): сглупил| сглупила} и начинаешь играть серьёзно.
     -> game

==game==

Приспешник: Эээ... Как это?.. # У
Лидер: Хм... # У
ЛидерПара: Да ладно, сейчас сдуется.
Приспешник: Точно. Это самое сложное место!
Приспешник: Брось! Не позорься! 

Ты проходишь то место, где засыпался Денис. 

Приспешник: Не может быть... # У
Настя: Да ладно... # У
Денис: Очков всё равно меньше! # З

Ты продолжаешь играть. 
И даже получаешь дополнительную жизнь. 

Настя: {isBoy(): Он тебя уделал| Она тебя уделала}. # У
Приспешник: Вау! Первый раз вижу этот уровень! # Р

Ты набираешь в 2 раза больше очков. 
И просто отходишь от автомата в сторону. 

Денис: Признаю, ты круто играешь.
Настя: Приятно видеть {isBoy(): нового нормального пацана| новую нормальную девчонку} в классе. 

- Я: Я {isBoy(): нормальный| нормальная} потому, что...
*    Я: ...не {isBoy(): испугался| испугалась} принять вызов? @good
     Лидер: Конечно! И потому что {isBoy(): показал| показала} хорошую игру.
     ~ ChangeRelPoints(Leader_RelPoints, 2)
     -> success

*    Я: ...хорошо играю? @good
     Лидер: И это тоже. А ещё потому что не {isBoy(): испугался| испугалась}.
     ~ ChangeRelPoints(Leader_RelPoints, 2)
     -> success

*    Я: ...хм... Почему же? @good
     Лидер: Потому что не {isBoy(): испугался и показал| испугалась и показала} хорошую игру!
     ~ ChangeRelPoints(Leader_RelPoints, 2)
     -> success

==success==

Лидер: Приходи сегодня вечером на место падения метеорита. 
Лидер: Придут только нормальные, ну ты понимаешь. 
ЛидерПара: Будем делать крутые слефи! # Р
Я: Я приду. 
Лидер: Замётано. Не опаздывай!

Ты неторопясь уходишь из игрового зала.
Хотя на самом деле, едва сдерживаешь волнение.
Не оборачиваясь заходишь в лифт и ждёшь пока закроются двери. 

# Location under_parking

Только выйдя из лифта, ты понимаешь, что {isBoy(): забыл| забыла} нажать на кнопку.
Лифт увёз тебя на подземную парковку.
И сразу уехал вверх. 

- Я: (Я могу...)
*    [(...дождаться лифта.)]
     Ты нажимаешь на кнопку вызова лифта. 
     Индикатор упрямо горит на втором этаже.
     Похоже, быстрее найти второй выход. 
    -> parking    
    
*    [(...найти другой выход с парковки.)]
     Ругая себя за невнимательность, ты отправляешься на поиск второго выхода. 
    -> parking

==parking==
Десептикон 1: Стой, {isBoy(): мальчик| девочка}!

Ты оборачиваешься, но никого не видишь. 
Вдруг прямо перед тобой останавливается автомобиль.

# Effect screen_shake

Ты чуть не падаешь от неожиданности.

Десептикон 1: Садись в машину. Я отвезу тебя в лабораторию. 
Я: Я вас раньше не видел. 
Десептикон 1: Да, меня попросили заехать за тобой. 
Я: А где Бамблби? 
Десептикон 1: Он не смог приехать. Садись скорее.

Ты идёшь к машине, прикрывая глаза от слепящего света фар. 
Вдруг тебе на телефон приходит сообщение. 

Бамблби: Прячься! Это десептикон!

Ты резко прыгаешь в сторону и прячешься между машинами. 

Десептикон 1: Ты всё-таки успел, немой автобот. 

Ты видишь, как машина превращается в огромного робота.

Десептикон 1: Давно мечтал разобрать тебя на запчасти!

Тут же напротив него вырастает Бамблби. 

Бамблби: ...

# Effect screen_shake

Десептикон 1: Как жаль, что мне запрещено показываться людям. 
Десептикон 1: Придётся убивать тебя тихо. 

Бамблби бросается на врага. 
Звон ударов сложно назвать тихой битвой. 
Но даже этот шум вряд ли кто-то услышит наверху.

- Я: (Я могу...)
*    [(...не высовываться.) @bad]
     Ты забиваешься в угол и ждёшь.
     
     Десептикон 1: Вот и всё! Автобот! # Р
     # Effect actor_shake
     
     Из руки десептикона вырастает длинное лезвие.
     Враг протыкает Бамблби насквозь.
     
     Десептикон 1: А теперь {isBoy(): мальчик| девочка}.
     # Effect actor_shake
     
     Десептикон хватает тебя огромной рукой и ты теряешь сознание.
     # Effect rewind
     -> DONE
     -> robot_battle
     
*    [(...помочь Бамблби.) @good]
     Десептикон теснит Бамблби. 
     Ты лихорадочно соображаешь, как помочь другу. 
     И замечаешь на противоположной стене кнопку пожарной сигнализации.
     
     Десептикон 1: Ты слабее, чем я думал, автобот! # Р
     Десептикон 1: И никто не узнает, что тут произошло! 
     
     Ты понимаешь, что нужно привлечь внимание. 
     - - Я: (Я могу...)
     * * [(...включить пожарную сигнализацию.) @good]
         Уличив момент, ты пробегаешь мимо трансформеров. 
         Оглушительный звон пожарной сигнализации наполняет всё вокруг. 
         
         Десептикон 1: Нет! # З
         Десептикон 1: Тебе повезло, немой автобот! 
         
         Десептикон трансформируется в машину и исчезает. 
         -> help_bamblbee
         
     * * [(...громко позвать на помощь.) @bad]
         Я: Помогите!!!
         Я: Кто-нибудь, помогите!!!
         
         В ответ ты слышишь лишь скрип железа. 
         Обернувшись, ты видишь, как Бамблби безжизненно падает на пол. 
         
         Десептикон 1: А теперь {isBoy(): мальчик| девочка}.
         # Effect actor_shake
         
         Десептикон хватает тебя огромной рукой и ты теряешь сознание.
         # Effect rewind
         -> DONE
         -> help_bamblbee
     
     ==help_bamblbee==
     -> robot_battle

==robot_battle==

Бамблби трансформируется в машину и открывает дверь. 
Ты прыгаешь внутрь. 

# Location bcar

Бамблби: Включить сигнализацию, было отличной идеей. 
Бамблби: Спасибо! # Р
Я: Кто это был?
Бамблби: <Имя десептикона 1.> Он десептикон. 
Я: А кто это? 
Бамблби: Они давние враги автоботов. Хотя и похожи на нас.
Я: И зачем он хотел меня похитить?
Бамблби: Ему нужен Защитник. 
Я: Та маленькая штучка, которую мне дали в Лаборатории? # У
Я: Зачем? 
Бамблби: Это не простая маленькая штучка. 
Бамблби: Внутри неё есть кое-что, что нужно всем трансформерам.
Я: А почему ты просто не заберёшь её себе? # У
Бамблби: Я один, а их много и они сильнее меня. # П
Бамблби: Надёжнее спрятать Защитника у тебя. 
Я: Но они могут меня похитить! Как сегодня. # З
Бамблби: Ты же {isBoy(): слышал| слышала}, что они прячутся.
Бамблби: Пока ты на виду — тебя не тронут.
Бамблби: А я буду рядом. 
Я: И в туалет тоже со мной будешь ходить?
Бамблби: Туалет? Это как сливать отработанное масло?
Я: Ха! Типо того! # Р
Я: Кстати, а что это за штука, которая им так нужна? 
Бамблби: Как-нибудь в другой раз расскажу. 
Бамблби: Ты уже дома, @playername@. Увидимся. 

Так и не получив ответа, ты направляешься домой. 

# Location room_main

Столько всего произошло за пару часов. 
На тебя напали десептиконы и признали одноклассники.
Но тут ты вспоминаешь, о разговоре с мамой. 
Интересно, что же она хотела тебе сказать?

->END