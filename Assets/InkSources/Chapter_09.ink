INCLUDE GameData.ink

-> start

=== start ===
# Location sport_club

Спортклуб тебе нравится. 
Здесь просторно и светло, чувствуется дух соревнований. 
Ты с удовольствием смотришь на тренировку команды. 

Тренер: Привет, {isBoy(): чемпион| чемпионка}! 

Тренер подходит незаметно. 

Тренер: Нравится?
Я: Здравствуйте! Очень нравится. # Р
Тренер: Я рад, но набор в подготовительную группу уже закончился. 
Тренер: Приходи на будущий год, в сентябре.
Я: Нет, мне не нужна подготовка.
Я: Я бы {isBoy(): хотел| хотела} записаться в команду.
Тренер: Сразу в команду? Любопытно. # У
Тренер: А почему я должен тебя взять? 

->sport_start
==sport_start==
- Я: (Я могу...)
*    [(...рассказать о своих успехах.)]
     Я: В прошлом году я {isBoy(): забил больше всех голов в| заняла второе место на} школьном чемпионате.
     Тренер: Неплохо. И как я могу это проверить?
     Я: Позвоните моему предыдущему тренеру. 
     Тренер: А как я пойму, что это именно тренер, а не твой отец?
     Я: А зачем мне вас обманывать? # У
     Тренер: Я не знаю, но такие случаи были. 
     Тренер: Мне нужно что-то больше, чем слова.
     ->sport_start

*    [(...показать свои способности.)]
     Я: Я могу показать на что {isBoy(): способен| способна}.
     Тренер: Это дело другое, но сейчас я занят. 
     Тренер: Тебе придётся подождать до конца тренировки. 
     Тренер: Ещё часа полтора. 
     Я: Хорошо. Я подожду. 
     ->sport_exit

*    [(...сказать, что тебе это очень важно.)]
     Я: Понимаете, я {isBoy(): должен| должна} обязательно попасть в команду.
     Я: Для меня это вопрос жизни и смерти. 
     Тренер: Тебя за дверями ждёт наёмный убийца? # У
     Я: Нет...
     Тренер: Тогда не морочь мне голову. 
     Тренер: У нас через несколько дней соревнования. 
     Тренер: А ты отвлекаешь меня от подготовки. 
     ->sport_start

==sport_exit==

Ожидая, пока закончится тренировка, ты пишешь {isBoy(): Вике| Вите}. 
Я: Что делаешь? 
Симпатия: Много чего. 
Я: А если подробнее. 
Симпатия: Сначала ты. 
Я: Ну, я сижу в спортклубе. 
Я: Жду, пока закончится тренировка. 
Симпатия: Зачем? 
Я: Потому что тренер так сказал. 
Я: Он посмотрит меня только после тренировки. 
Симпатия: Ясно. 
Я: Твоя очередь? 
Симпатия: Очередь для чего? 
Я: Рассказать, что ты делаешь.
Симпатия: Ну... Как тебе сказать...
Я: Скажи как есть.
Симпатия: Ладно. 
Симпатия: Я учу нубов как правильно агрить мобов. 

- Я: Эээ...
*    Я: ...а зачем их учить? # У
     Симпатия: Чтобы вместе пройти данж. 
     Я: А ты сама не справишься? 
     Симпатия: Да что ты понимаешь...
     Я: Ну, кое-что я понимаю. 
     Симпатия: Правда? Давай проверим. # У
     Я: Давай, только в другой раз.
     ->game_slang

*    Я: ...что за бред ты несёшь? @bad # З
     Симпатия: Если ты не понимаешь, это сразу бред? # З
     Я: Но ведь это действительно бред! 
     Я: Тарабарщина какая-то. 
     Симпатия: Бред, так бред. Думай, как хочешь. 
     ~ ChangeRelPoints(Love_RelPoints, -2)
     ->game_slang

*    Я: ...хоть я ничего не {isBoy(): понял| поняла}, это звучит интересно. @good
     Симпатия: Правда? # У
     Я: Конечно. Ты кого-то учишь, значит, это им нужно. 
     Я: И значит, что ты в этом хорошо разбираешься. 
     Симпатия: Ты прав, разбираюсь. 
     Симпатия: Если захочешь и тебя научу. 
     Я: С радостью. 
     Симпатия: Договорились, но позже. 
     ~ ChangeRelPoints(Love_RelPoints, 2)
     ->game_slang

==game_slang==
Я: Так, тут тренировка заканчивается. 
Я: Пойду разминаться. 
Симпатия: Удачи! 

Сперва тренер просит тебя сдать нормативы.
Тренер: Так, бег, прыжки с места и через скакалку. 
Тренер: Что первое? 

VAR sport = 0

->sport_test
==sport_test==
- Я: Давайте...
*    ...бег. 
     Ты пробегаешь стометровку. 
     Тренер: {isBoy(): 13,5| 16} секунд. # У
     Тренер: Первый юношеский разряд. Отлично. # Р
     ~ sport++
     ->sport_test

*    ...прыжки с места.
     Ты прыгаешь. 
     Тренер: {isBoy(): 2,3| 1,9} метра. 
     Тренер: Очень хорошо. # Р
     ~ sport++
     ->sport_test

*    ...прыжки через скакалку.
     Ты всегда {isBoy(): любил| любила} прыгать через скакалку. 
     Тренер: Ого... {isBoy(): 74| 82} раза за 30 секунд. # У
     Тренер: Великолепно. # Р
     ~ sport++
     ->sport_test
     
+    ...закончим.
     {
     - sport > 2 : 
         ->sport_test_exit
     - else:
         Тренер: Нет, нужно сдать все нормативы. 
         ->sport_test
     }

==sport_test_exit==

Тренер: Что ж, неплохо. # У
Тренер: Но хороших физических данных мало.
Тренер: Покажи, что ты умеешь. Удиви меня. 

->before_trainer_test
==before_trainer_test==
- Я: (Я могу...)
+    [(...{isBoy(): Ударить по воротам| Сесть на шпагат}.)]
     {isBoy(): Ты разбегаешься, бьешь по мячу и попадаешь точно в девятку| Ты прямо перед тренером садишься на поперечный шпагат}.
     Тренер: Неплохо, но так в моей команде может каждый. 
     # Effect rewind
     ->before_trainer_test

+    [(...{isBoy(): Обвести тренера| Сыграть в зеркало с тренером}.)]
     Тренер: Хочешь {isBoy(): обвести меня| повторить все мои движения}?
     Тренер: Любопытно. 
     Ты держишься долго, минут пять.
     Только потом тренеру удаётся {isBoy(): отобрать у тебя мяч| сбить тебя с толку}. 
     Тренер улыбается и тяжело дышит. 
     
     ->trainer

+    [(...{isBoy(): Чеканить мячь ногой| Сделать быструю серию шагов}.)]
     {isBoy(): Ты чеканишь мяч| Ты начинашь делать шаги}.
     Тренер с минуту смотрит на тебя, потом останавливает. 
     Тренер: Да, ловко, но не удивительно. 
     Тренер: У меня в команде таких хватает. 
     ->before_trainer_test

==trainer==

Тренер: Ты мне нравишься! 
Тренер: Могу взять тебя в запасной состав.
Я: А почему в запасной? # У 
Я: Вам же понравилось. 
Тренер: Да, понравилось, но я тебя не знаю. 
Тренер: Вдруг на соревнованиях ты растеряешься?
Тренер: Чтобы взять тебя в команду, я должен быть уверен на 100%. 
Я: Я докажу! 
Тренер: Кстати, ты в курсе, что у нас каждый сам форму покупает?
Я: Да. Но ведь тренироваться можно и так.
Тренер: У меня — нет. Не люблю, когда пестрит в глазах. 
Тренер: Купишь форму — приходи на тренировку. 
Я: То есть вы берёте меня в команду? 
Тренер: Пока нет. Возьму, если докажешь, что стоишь этого. 
Я: Ага, я куплю форму, а вы меня не возьмете... # П
Тренер: Зато будет стимул выложиться по полной. 
Тренер: Всё. Разговор окончен. 

Ты собираешь вещи, понимая, что всё потеряно.
Отец не купит форму, пока тебя не возьмут в команду. 
А в команду тебя не возьмут, пока ты не купишь форму.

# Location sport_parking

На парковке перед спортклубом пустынно. 
От этого тебе становится ещё грустнее.
Вдруг ты видишь {isBoy(): Вику, бредущую| Витю, бредущего} по улице.

Я: {isBoy(): Вика| Витя}! # Р
Симпатия: О, неожиданно. # У
Я: Действительно. Как твои мобонубы? 
Я: Научились чему-нибудь? 
Симпатия: Во-первых, это два разных слова. 
Симпатия: Мобы и нубы. 
Симпатия: Во-вторых, не научились. Свет вырубило. 
Я: А в темноте нельзя учиться? # Р
Симпатия: Ты что, реально не въезжаешь? # У

- Я: Если честно, то...
*    Я: ...совсем не понимаю. 
     Симпатия: Да, ты даже не нуб. Ты хуже. 
     Я: Может расскажешь, что всё это значит? 
     ->dont_understand

*    Я: ...я просто прикалываюсь над тобой.
     Симпатия: То есть ты делаешь вид, что не шаришь? # У
     Я: Типа того.
     Симпатия: И чем же я по-твоему {isBoy(): занималась| занимался}?
     
     - - Я: Ну...
     * * Я: ...Ты учила мобов аргиться на нубов. @bad
         Симпатия: Агриться, а не аргиться! # З
         Симпатия: И нубов на мобов, а не мобов на нубов! # З
         Симпатия: Не умеешь, не ври. # З
         ~ ChangeRelPoints(Love_RelPoints, -2)
         Я: Ладно, признаю. Я ни слова не понимаю. # П
         ->dont_understand
            
     * *  Я: ...Ты кого-то чему-то учила. @bad
         Симпатия: Как бы так сказать, чтобы ничего сказать.
         Я: В смысле? # У
         Симпатия: В прямом! 
         Симпатия: Если не знаешь, признайся сразу. 
         ~ ChangeRelPoints(Love_RelPoints, -2)
         Я: Ты права. Я не понимаю, о чём речь. # П
         ->dont_understand
            
     * *  Я: ...Ты готовила себе помощников. @good
         Симпатия: И каких же помощников? 
         Я: Нубов — неопытных игроков. 
         Я: Чтобы они отвлекли сильных мобов — монстров — на себя.
         Я: А ты смогла пройти дальше и собрать лут. 
         Симпатия: Плюс 10 к твоей экспе! # Р
         ~ ChangeRelPoints(Love_RelPoints, 2)
         ->understand
            
*    Я: ...я просто пытаюсь поддержать разговор. @good
     Симпатия: Зачем?
     - - Я: Потому что...
     * *     Я: Ну... Ты мне нравишься. @good
             Симпатия: Серьёзно? # У
             Симпатия: Ладно, прощаю твою неосведомлённость. 
             ~ ChangeRelPoints(Love_RelPoints, 2)
             Я: Спасибо. # Р
             Я: Так что значат эти странные слова?
             ->dont_understand
     
     * *     Я: Я не люблю неловкое молчание. @bad 
             Симпатия: А что, обязательно нужно говорить?
             Я: Не знаю... Но мне в тишине неуютно. 
             Симпатия: Тогда продолжай разговор. 
             Я: Эээ...
             Я: Что значат эти странные слова?
             ->dont_understand
     
     ->dont_understand

==dont_understand==
Симпатия: Это сленг онлайн-игр. 
Симпатия: Нуб — это начинающий игрок. 
Симпатия: Моб — это компьютерный персонаж, обычно монстр. 
Симпатия: Агрить — это отвлекать моба на себя. 
Я: А зачем? 
Симпатия: Чтобы моб не сагрился на других игроков. 
Я: Что сделал? 
Симпатия: Не напал на других. Это командная игра. 
Я: Ох... как всё сложно...
Симпатия: Когда разберешься — нормально. 

->understand
==understand==
Вы проходите некоторое время молча. 
{isBoy(): Вика хмурая, неразговорчивая| Витя хмурый неразговорчивый}.

Я: Слушай, я что-то после тренировки {isBoy(): устал| устала}.
Я: Может в кафе посидим? 
Симпатия: Можно. 

# Location cafe

Я: Какое мороженое будешь? 
Симпатия: Чёрное. 
Я: Как твоё настроение?
Симпатия: Типа того...
Я: А что случилось-то? 
Симпатия: Я же {isBoy(): говорила| говорил}, что свет вырубило. 
Я: Так бывает. 
Я: Разве это повод для грусти? 
Симпатия: У нас была такая игра...# П
Симпатия: Мы почти прошли новый данж, понимаешь? 
Симпатия: Там можно было столько лута и экспы набить...
Симпатия: Я почти ап {isBoy(): словила| словил}. # П
Я: Вижу, для тебя это очень важно. 
Симпатия: Конечно! Это моя жизнь!
Симпатия: Веришь, я иногда во сне вижу себя своим персом.

- Я: Подожди...
*    Я: ...компьютерные игры — вся твоя жизнь?
     Симпатия: Не вся, но большая её часть. 
     ->games_vs_life

*    Я: ...у тебя зависимость от компьютерных игр? @bad
     Симпатия: Ненавижу, когда кто-то говорит про зависимость. # З
     Симпатия: Её не существует! Это тупые страшилки! # З
     Симпатия: Ты знаешь, что в 19-м веке детям запрещали читать? 
     Я: Да ладно. # У
     Симпатия: А ты погугли. 
     Симпатия: Считалось, что дети слишком много времени проводят за чтением.
     Симпатия: Что это вредно для здоровья. 
     Симпатия: Потом пугалом сделали телевизор. 
     Симпатия: А теперь — компьютерные игры! # З
     ->games_vs_life

*    Я: ...значит, со мной тебе не интересно? @good
     Симпатия: Почему? # У
     Я: Сны — отражение наших мыслей. 
     Я: Мы видим там то, что нас занимает.
     Я: Я бы с удовольствием {isBoy(): увидел| увидела} во сне тебя.
     Симпатия: Ну...
     ~ ChangeRelPoints(Love_RelPoints, 2)
     Симпатия: Наверное, ты {isBoy(): прав| права}. # П
     Симпатия: Я слишком много времени провожу там... # П
     ->games_vs_life

==games_vs_life==
Я: И сколько ты обычно играешь?
Симпатия: По-разному.
Симпатия: Обычно 2–3 часа, а на выходных больше.
Я: Насколько? 
Симпатия: Если маман не нужна помощь, то...
Симпатия: Однажды {isBoy(): просидела| просидел} с утра субботы до обеда воскресенья.
Я: Больше суток подряд?! # У
Симпатия: А что такого? 
Симпатия: Это были самые счастливые 29 часов моей жизни.
Я: А я сегодня смогу записать на свой счёт счастливых полчаса. 
Симпатия: И что же это были за полчаса? 
Я: Они ещё продолжаются. Прямо сейчас. 
Симпатия: Ты меня смущаешь... # Р
Симпатия: Кстати, как пообщался с тренером? 
Я: Ужасно. # П
Симпатия: Что такое? Он тебя не стал слушать? 
Я: Нет. Даже посмотрел и сказал, что у меня есть способности. 
Я: Но в команду не взял. 
Я: Сказал, что обязательно нужно купить форму. 
Симпатия: Так купи. Это проблема? 
Я: Да. Отец поставил условие. 
Я: Он купит форму, только если меня точно возьмут в команду. 
Симпатия: Да уж... Замкнутый круг... # П

Некоторое время вы молча едите мороженное. 
Молчание становится невыносимым. 
Вдруг {isBoy(): Вика| Витя} решительно встаёт из-за стола. 

Симпатия: Я поговорю с маман. 
Я: Не {isBoy(): понял| поняла}. # У
Симпатия: Моя маман хорошо знает это тренера. 
Симпатия: Они вместе когда-то учились в спортшколе. 
Симпатия: Я попрошу её поговорить с ним. 
Я: Серьёзно? # У
Симпатия: Так же серьёзно, как варвар 80-го уровня! # Р
Симпатия: Я тебе вечером напишу. 
Вы прощаетесь и ты отправляешься домой.

# Location kitchen

Дома ты застаёшь отца за ужином. 
Отец: Помнится, ты сегодня {isBoy(): должен был| должна была} идти в спортклуб?
Отец: Как прошло?

- Я: (Я могу...)
*    [(...соврать, что меня взяли в команду.) @bad]
     Я: Отлично. Меня берут в команду. 
     Отец: Что-то не вижу радости на лице? # У
     Отец: Ты {isBoy(): недоволен| недовольна}?
     Я: Доволен. Просто {isBoy(): устал| устала}.
     Отец: У тебя когда завтра тренировка? 
     Отец: Я хочу прийти посмотреть на тренера. 
     Я: Не надо... Он этого не любит. 
     Отец: Пусть не любит. Это моя обязанность, как родителя. 
     Отец: Я должен быть уверен, что тебя там не обидят. 
     Отец: Или ты мне врёшь?
     Я: Ну {isBoy(): соврал| соврала}... да! # З
     ~ ChangeRelPoints(Parents_RelPoints, -2)
     Отец: Я сразу понял. Рассказывай. 
     ->dad_sport

*    [(...ещё раз попытаться уговорить отца купить форму.)]
     Я: Пап, может всё-таки купим форму? 
     Отец: Мы уже об этом говорили. 
     Я: Все тренируются в форме.
     Я: Я буду там как белая ворона.
     Отец: Так это ж хорошо! # Р
     Отец: Тренер будет сразу выделять тебя из толпы. 
     Я: Ну, пап... Давай купим, а? 
     Отец: Чего это ты снова {isBoy(): взялся| взялась} клянчить?
     Отец: Это неспроста. Выкладывай. 
     ->dad_sport

*    [(...рассказать отцу про тренера. @good)]
     Я: Я {isBoy(): показал| показала} всё, что умею. 
     Я: Ему, вроде, понравилось. 
     Отец: Вроде? # У
     Я: Да, он даже сказал, что готов взять меня в запасной состав.
     Отец: Так это же классно!
     Я: Нет. Я хочу в основной. 
     Отец: А он что? 
     Я: Тебе всё-всё рассказать? 
     Отец: Конечно. Я очень ценю, что ты делишься со мной. 
     ~ ChangeRelPoints(Parents_RelPoints, 2)
     ->dad_sport

==dad_sport==

Ты пересказываешь отцу требования тренера. 

Отец: Что сказать? Оригинально... # П
Отец: Знаешь, мне не нравится такой подход. 
Отец: Если хочешь, я завтра с ним поговорю. 
Я: Не надо. Попробую {isBoy(): сам| сама} решить. 
Отец: Как знаешь. Но моё предложение не меняется. 
Отец: Форма — только после официального зачисления в команду. 

# Location room_main

Весь вечер ты никак не можешь сосредоточиться на уроках. 
Голова занята переживаниями по поводу тренировок. 
Сообщение от {isBoy(): Вики| Вити} приходит уже затемно.

Симпатия: Не спишь?
Я: Уснёшь тут, как же. Волнуюсь. 
Симпатия: Спи спокойно, маман договорилась с тренером. 
Я: Не шутишь? # У
Симпатия: Юмор — не мой конёк. Всё серьёзно. 
Симпатия: Послезавтра тренировка в 14:00. Не опаздывать!
Я: Так точно! # Р
Я: Спасибо тебе огромнющее! # Р
Симпатия: Пользуйся. 
Я: Слушай, вот у тебя мама ходила в спортшколу. 
Я: Лично знает тренера. 
Я: Почему ты не ходишь в секцию?
Симпатия: Спорт — для тех, кто не любит думать.
Я: Ну, спасибо... # П
Симпатия: Ой... Не принимай близко к сердцу. 
Симпатия: Я вот свою маман очень люблю. 
Симпатия: Хотя она вся такая спортивная.
Симпатия: Ты, кстати, тоже...
Я: Приятно слышать.
Симпатия: Просто у меня сложные отношения со спортом...
Симпатия: Я не люблю всю эту активность...
Симпатия: Спокойной ночи! 
Я: Спокойной. 

Перед тем, как выключить телефон, ты заглядываешь в общий чат. 
Там тихо.
Неожиданно появляется сообщение {isBoy(): Дениса| Насти}.

Лидер: Только попадись он мне! # З
Лидер: Убью!!! Размажу по стенке!!! # З

После этого {isBoy(): Денис| Настя} уходит в офлайн. 

->END
