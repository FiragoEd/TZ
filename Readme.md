Сутью задания было исправить баги и найти коммон ошибк, и реализовать пару фитч. Понятие коммон ошибок достаточно расплывчето) А добавление DI контейнера было овер для текущего тз , поэтому я обратил внимание на некоторые системы и легко перефакторил некоторые из них. Я думаю нелогично было бы рефакторить все , если это не было указано :D)))
## Доп описание
- Startup Config ( чтобы прокинуть стартовый конфиг)
    С использованием DI, это момент легко решиться, поверп систему использовал конфиги сет конфиги
- Скорее всего использование Юнити ивентов приведет к дальнейшим проблемам
- Проджект тайл имеет свой бихейвер, не хотелось бы иметь лишний оверхед на упдейт.
- EventBus через cтринги( Наверное лучше детерменировать все возможные переменные, я думаю евент менедежр через интерфейсы пушка)
- Пулл система была взята рандомно, не видел особо большо смысла генерировать что-то свое, для текущих задач более чем хватит ( Если говорить про проекты с DI - контейнером(Zenject, не говорю про "Диай для бедных")), то конечно надо было б сделать качественный диайный пулл)
- Веапон система конечно подлежит фулл рефакторингу( рейкасты и было бы прикольно напичкать скейллбл паттернами типа визитр)
- Ну и конечно идея инверсии зависимости тут вообще не просматривается( все тягает что попало) проджектайлы плаят
