INCLUDE GameData.ink

-> start

=== start ===

# Location: Комната м
Ты сидишь в своей комнате
Раздается стук в дверь
Я: Войдите
В комнату заходит Стас
Я: Ну наконец то!
Я: Быстрее, надо готовиться к контрольной
Стас: Хорошо!
Вы занимались 2 часа без остановки и наконец сделали перерыв
Стас: Ну, как тебе в нашем классе? 
Я: Неплохо. 
Стас: Да, у нас неплохой класс, вообще-то... 
Я: Ты оправдываешься? 
Стас: {isGenderBoy(): Ну, как сказать. Ты же слышал, что меня...  | Ну, как сказать. Ты же видела... как меня…} # грусть
Я: Назвали Свином?
Стас: Типа да. Мне не нравится, но я терплю.
->dialog1

===dialog1===

Я: Слушай… #defenderAvailable: true
	*[Почему тебя называют Свином? @neutral]
	Стас: Ну, была одна история с видео. # грусть
	Я: Не хочешь, не рассказывай.
	Стас: Ты всё равно узнаешь. Лучше уж я расскажу сам. 
	Стас: Был случай, я упал в грязь, и это сняли на видео. 
	Я: И что? 
	Стас: Выложили на YouTube и всему классу разослали ссылку. # злость
	Я: {isGenderBoy():  Я видел подобные ролики. Это забавно.  | Я видела подобные ролики. Это забавно. }
	Стас: Ага, когда смотришь на других. А когда это ты сам весь в грязи и не можешь встать… # грусть
	Стас: Надо мной половина школы смеялась… С тех пор и кличка эта дурацкая… # грусть
	Я: Прости. Это, действительно неприятно… # грусть
	->dialog1
	
	*[Кто придумал тебе такую кличку? @good]
	Стас: {isGenderBoy(): Денис. Его все боятся в классе. | Настя. Она в классе самая крутая.}
	Я: Мне так не показалось. 
	Стас: Ещё не вечер. Советую тебе не расслабляться. 
	Я: Да я всегда на чеку.
	->dialog1
	*[Мне уже пора спать, давай завтра продолжим... @bad]
    ->Conflict01
    
    ===Conflict01===
    Конец ознакомительного фрагмента
    ->DONE

->END