INCLUDE GameData.ink

-> start

=== start ===

# Location: Комната м
-> dialog1

=== dialog1 ===
    Я окликнул Стаса

    { 
        - Stas_RelPoints > 10:
        Стас: Привет, рад тебя видеть! # радость
        - Stas_RelPoints < 5:
        Стас: Пошёл отсюда, тебя никто не звал! # злость
        - else:
        Стас: Привет
    }
    
	+ [Улучшить отношения]
	~ ChangeRelPoints(Stas_RelPoints, 2)
	Вы улучшили отношения
	Стас: Я буду относится к тебе лучше
	Текущий уровень отношений - { Stas_RelPoints }
	-> dialog1
	
	+ [Ухудшить отношения]
	~ ChangeRelPoints(Stas_RelPoints, -2)
	Стас: Я буду относится к тебе хуже
	Текущий уровень отношений - { Stas_RelPoints }
	-> dialog1
	
	+ { Stas_RelPoints <= 5 } 
	[Мы ненавидим друг друга]
    Стас: Да, терпеть тебя не могу.
    -> dialog1
        
    + { Stas_RelPoints >= 10 } 
    [Мы отлично ладим]
    Стас: Да, мы лучшие друзья
    -> dialog1
	
	* [Выйти]
    -> ToExit
    
    === ToExit ===
    Конец ознакомительного фрагмента
    ->DONE

->END