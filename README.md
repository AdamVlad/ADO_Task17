# ADO_Task17

Задание 1. Подключение двух разных источников данных в проекте
Разработайте приложение, в котором будет подключено два разных источника данных: MSSQLLocalDB и MS Access.
Организуйте подключение, выведите строку подключения и статус подключения к этим источникам данных. 
Вывод данных выполните в графическом интерфейсе. Также необходимо учесть, что должна быть авторизация по логину и паролю для источника данных.

Задание 2. Создание таблиц с данными
Расширим программу из задания 1. Предположим, что в разных источниках данных храниться информация из двух систем, информацию из них необходимо сверять между собой, чтобы находить и выводить недостающую. Создайте и заполните таблицы произвольными данными для решения задачи. 

Первый источник данных должен содержать таблицу с полями:

ID
Фамилия
Имя
Отчество
Номер телефона (может быть пустым)
Email.
Второй источник данных содержит таблицу со следующими полями:

ID
Email
Код товара
Наименование товара
У нас две разные системы, которые хранят разную информацию по клиентам. Одно из полей должно быть идентификатором. В нашем случае — это поле e-mail.

Задание 3. Разработка программы для онлайн-магазина
Расширим программу из задания 2. Создайте запросы SQL:

Select — для выборки данных о покупках по клиенту.
Insert — вставка во вторую таблицу по совершенной покупке, а также добавление новых клиентов в первую таблицу.
Update — обновление данных по клиенту из первой таблицы.
Delete — очистка таблиц.
После чего, используя запросы SQL, разработайте программу для сотрудников магазина.