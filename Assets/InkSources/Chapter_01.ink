VAR didCompleteChapter = false
VAR expGain = 0
-> start

EXTERNAL isGenderBoy()

=== function isGenderBoy() ===
    ~ return true

=== start ===

Location: {isGenderBoy(): Комната м | Комната д}

{isGenderBoy(): Ты далеко не любитель рано просыпаться | Ты далеко не любительница рано просыпаться}
А просыпаться 1 сентября - тем более.
Первые десять минут ты обычно просто лежишь и тупишь в смартфон.
Это помогает проснуться.
В это утро остатки сна покинули тебя раньше обычного,
Потому что тебе пришло странное сообщение:

Location: Телефон

«Смотри скорее, там такое…»
Я: О, это еще что? # удивление
Я: Какие-то новости из старой школы? # удивление
Я: Больше похоже на какой-то прикол. 
Но любопытство взяло верх...
Тебя перекинуло на страницу ВК.
Я: “Введите логин-пароль”...
Я: Зашибись! # злость
Я: Еще вчера все работало!
Я: Окей, вот тебе мой логин-пароль.
Но стоило ввести логин-пароль и нажать “ок”...
Как смартфон загадочно подмигнул тебе и перезагрузился.

Location: {isGenderBoy() : Комната М | Комната Д}

Я: Класс! Батарея села! # злость
Я: Самое время.
Тук-тук!
Мама: Проснулось, Красно-солнышко? # радость
Мама: Бегом одеваться! Завтрак стынет!

-> kitchen

=== kitchen ===

Location: Кухня

На кухне солнечно и приятно пахнет блинами. 
Ты хватаешь еще горячий блин и макаешь его в варенье.
Мама: Ну, как блины? # радость
Я: Отлично, мам. # радость
Мама всегда поддерживает тебя.
Сегодня она специально встала пораньше, чтобы приготовить твои любимые блины.
Она очень хочет быть твоим другом.
Но для тебя она все-таки больше мама.
Мама: Волнуешься в свой первый день в школе?
Я: Ну... #defenderAvailable: false 
    * [Нет, с чего бы?]
        ->Conflict01
    * [ Да, я боюсь облажаться…]
        ->Conflict01

== Conflict01 ==
Мама: Ой, брось. Волноваться - это нормально. # радость
Мама: Я три раза меняла школу. # радость
Мама: Только запомни:
Мама: Если тебя задирают - не бойся дать сдачи,
Мама: И рассказать взрослым о своих проблемах.
Я: Спасибо, мам. # печаль
Я: Ты умеешь успокоить. # печаль
Мама: А как твои друзья? Писали тебе летом?
Я: Угу. Сегодня вот один прислал…
{isGenderBoy(): Ты вспомнил то странное сообщение!|Ты вспомнила то странное сообщение!}
Интересно, что там?
Я: Мам, спасибо! Мне пора!
Мама: Так, погоди! # удивление
Мама: Помнишь, о чем мы с тобой договаривались? # злость
Мама: Теперь ты сам моешь тарелку за собой.
Я: Ну мам... # печаль #defenderAvailable: false 
    *[Только не сегодня…]
        ->Conflict02
    *[Ладно, вымою…]
        ->Conflict02
== Conflict02 ==
Мама: И не надо на меня так смотреть! 
Мама: Это не я люблю качать права, что “уже не ребенок”.
Тебе пришлось вымыть посуду.
Наверное, не случилось бы ничего плохого,
Оставь ты это до вечера...
Но уговор есть уговор.
Мама: Теперь я вижу, что у меня растет настоящий помощник! # радость

Location: {isGenderBoy() : Комната м | Комната д}

Смартфон уже должен был зарядиться.
Тебе не терпелось прочесть наконец сообщение.
Только твоя рука потянулась за смартфоном, как…
Мама: Глазам не верю! # удивление
Мама: Мы только три недели назад как переехали, # удивление
Мама: а у тебя опять на полу носки и бумажки разбросаны! # удивление
Я: Я все уберу вечером!
Мама: Нет, давай уж уберись сейчас. # злость
Мама: Кто из нас “уже не ребенок”?
Я: Мам... #defenderAvailable: false 
    *[Сейчас мигом тут все уберу!]
        Мама: Умничка! # радость
        Мама: А теперь бегом в школу!
        ->Conflict03
        *[Я опаздываю в школу!]
        Мама: Знаю я твои “опаздываю”. # злость
        Мама: Что ж, беги тогда.
        ->Conflict03

== Conflict03 ==

Мама: И посмотри, не пришли ли квитанции за газ!

Location: Подъезд

Я: Ну наконец-то я прочитаю что там!
Я: Что за фигня? # удивление
Я: Да как так “пароль неверный”?! # удивление
Я: Не могли же у меня за полчаса угнать пароль! # злость
Раз за разом выходило одно и то же: “Неверный логин или пароль”!
Тебе еще и пришлось копаться в почтовом ящике
Похоже, почтальон путает его с мусорной корзиной.
Столько бумажек!
На пол выпала яркая красно-зеленая листовка.
Я: Хм… А это еще что? # удивление
Я: «Лаборатория Компьютерной Безопасности. У вас проблемы? Мы их решим!»

~ expGain = expGain + 1
~ didCompleteChapter = true
->DONE

->END