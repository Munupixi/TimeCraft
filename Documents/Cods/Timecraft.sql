CREATE DATABASE Time_Craft;


CREATE TABLE IF NOT EXISTS "User" (
    "UserId" SERIAL PRIMARY KEY,
    "Login" VARCHAR(50) UNIQUE NOT NULL,
    "Password" VARCHAR(20) NOT NULL,
    "Name" VARCHAR(20) NOT NULL,
    "Surname" VARCHAR(50),
    "Patronymic" VARCHAR(50),
    "Age" INT NOT NULL
);
CREATE TABLE IF NOT EXISTS "Category" (
    "CategoryId" SERIAL PRIMARY KEY,
    "Title" VARCHAR(35) UNIQUE NOT NULL,
    "Description" TEXT, 
  "Color" VARCHAR(50)
);

CREATE TABLE IF NOT EXISTS "Task" (
    "TaskId" SERIAL PRIMARY KEY,
    "Title" VARCHAR(155) UNIQUE NOT NULL,
    "Description" TEXT,
    "StartDate" DATE,
    "StartTime" TIME,
    "Repeat"  INT,
    "EndDate" DATE,
    "EndTime" TIME,
    "Priority" INT,
    "IsDone" BOOL NOT NULL,
    "CategoryId" INT,
    "UserId" INT NOT NULL,
    FOREIGN KEY ("CategoryId") REFERENCES "Category"("CategoryId"),
    FOREIGN KEY ("UserId") REFERENCES "User"("UserId")
);

CREATE TABLE IF NOT EXISTS "Event" (
    "EventId" SERIAL PRIMARY KEY,
    "Title" VARCHAR(155) UNIQUE NOT NULL,
    "Description" TEXT,
    "StartDate" DATE NOT NULL,
    "StartTime" TIME NOT NULL,
    "EndDate" DATE NOT NULL,
    "EndTime" TIME NOT NULL,
    "Location" VARCHAR(100),
    "DressCode"  INT,
    "Priority" INT,
    "CategoryId" INT,
    "UserId" INT NOT NULL,
    FOREIGN KEY ("CategoryId") REFERENCES "Category"("CategoryId"),
    FOREIGN KEY ("UserId") REFERENCES "User" ("UserId")
);

CREATE TABLE IF NOT EXISTS "Participant" (
    "ParticipantId" SERIAL PRIMARY KEY,
    "IsAccepted" BOOL NOT NULL,
    "Role" VARCHAR(30),
    "IdEvent" INT NOT NULL,
    "IdUser" INT NOT NULL,
    FOREIGN KEY ("IdEvent") REFERENCES "Event"("EventId"),
    FOREIGN KEY ("IdUser") REFERENCES "User"("UserId")
);


INSERT INTO "User" ("Login", "Password", "Name", "Surname", "Patronymic", "Age") VALUES
('Artur2005@gmail.com', 'Turbanik11!!', 'Артур', 'Хайретдинов', 'Русланович', 30),
('George2005@gmail.com', 'Dsdn12!!rrgwe', 'Георгий', 'Борисов', 'Михайлович', 25),
('Faza2005@gmail.com', 'Jioqw12$q2', 'Фаяз', 'Биктимеров', 'Фанилович', 77),
('Aleksandrovna@gmail.com', 'Passw2o4d@12', 'Елена', 'Смирнова', 'Александровна', 28),
('Dimka12o@gmail.com', 'Pass34d@12', 'Дмитрий', 'Иванов', 'Васильевич', 40),
('PetrovaOlga@gmail.com', 'WffE12W21!!', 'Ольга', 'Петрова', 'Викторовна', 33),
('Maria1212@gmail.com', 'QAw1212!wq', 'Мария', 'Козлова', NULL, 28),
('MelByrim@gmail.com', 'GFw2323!@#dw', 'Андрей', 'Бурим', NULL, 6),
('EkaterinaMiz@gmail.com', 'qwas1212qQa!!', 'Екатерина', 'Мизулинна', NULL, 30),
('PavlikOpOP@gmail.com', 'ewWE2323!21', 'Павел', 'Кузнецов', 'Александрович', 25),
('Svetka1995@gmail.com', 'D2312wedw2@3', 'Светлана', 'Смирнова', 'Владимировна', 37),
('IrinaPopova@gmail.com', 'errt214AA222@@', 'Ирина', 'Попова', 'Васильевна', 8),
('LebedevAlecsandr@gmail.com', 'egerEf12#@rf', 'Александр', 'Лебедев', NULL, 32),
('Volkovv@gmail.com', '34nnJe#frf', 'Евгений', 'Волков', NULL, 28),
('TatyanaSol@gmail.com', 'Dveib12h$ee', 'Татьяна', 'Соловьева', NULL, 80);


INSERT INTO "Category" ("Title", "Description", "Color") 
VALUES 
('Семейные мероприятия', 'Мероприятия, организованные для семейного времяпровождения и веселья', 'blue'),
('Кулинария', 'Мероприятия, связанные с готовкой и кулинарией', 'green'),
('Творчество и ремесла', 'Мероприятия, посвященные творческому творчеству и ремесленным делам', 'orange'),
('Спорт и активный отдых', 'Мероприятия, связанные с физическими упражнениями и активным отдыхом', 'red'),
('Природа и путешествия', 'Мероприятия, направленные на исследование природы и путешествия', 'green'),
('Культура и искусство', 'Мероприятия, посвященные искусству, культуре и истории', 'purple'),
('Развлечения и аттракции', 'Мероприятия для развлечения и отдыха, включая аттракционы и развлекательные центры', 'yellow'),
('Образование и саморазвитие', 'Мероприятия, связанные с обучением, саморазвитием и получением новых знаний', 'blue'),
('Добровольчество и помощь', 'Мероприятия, связанные с добровольческой работой и помощью нуждающимся', 'orange'),
('Здоровье и благополучие', 'Мероприятия, направленные на поддержание здоровья и благополучия', 'red'),
('Специальные мероприятия', 'Мероприятия, которые не попадают под другие категории', 'grey');


INSERT INTO "Task" ("Title", "Description", "StartDate", "StartTime", "Repeat", "EndDate", "EndTime", "Priority", "IsDone", "CategoryId", "UserId") VALUES
('Расследование старых фотографий', 'Исследование старых семейных альбомов в поисках удивительных историй.', '2024-04-02', '10:20:00', 0, NULL, NULL, 2, FALSE, 1, 1),
('Приготовление экзотических блюд', 'Эксперименты на кухне с непривычными ингредиентами и вкусами.', '2024-04-01', '11:00:00', 1, '2024-04-04', NULL, 3, FALSE, 3, 2),
('Поход в ботанический сад', 'Прогулка среди красочных растений для вдохновения и умиротворения.', '2024-04-03', '12:00:00', 0, NULL, NULL, 1, FALSE, 4, 4),
('Мастер-класс по рисованию', 'Обучение техникам искусства для развития креативности и мастерства.', '2024-04-04', '13:00:00', 20, NULL, NULL, 4, FALSE, 6, 4),
('Ликвидация ненужных вещей', 'Освобождение пространства и улучшение организации жизни.', '2024-04-05', '14:00:00', 0, NULL, NULL, 1, FALSE, 6, 1),
('Фотосессия на природе', 'Создание красивых и запоминающихся образов в естественной среде.', '2024-04-06', '15:00:00', 31, '2024-04-09', '15:34:00', 3, TRUE, 3, 2),
('День спонтанных приключений', 'Поиск неожиданных и захватывающих приключений без предварительного плана.', '2024-04-07', '16:00:00', 1, NULL, NULL, 2, FALSE, 7, 3),
('Поиск сокровищ на блошиных рынках', 'Поиск уникальных предметов и антиквариата на рынках.', '2024-04-08', '17:00:00', 0, NULL, NULL, 2, FALSE, 8, 4),
('Путешествие на велосипедах', 'Поездка на велосипеде для активного отдыха и исследования местности.', '2024-04-09', '10:20:00', 32, NULL, NULL, 2, FALSE, 7, 5),
('Медитация на закате', 'Погружение в гармонию с природой и самим собой в закатных лучах.', '2024-04-08', '11:00:00', 1, '2024-04-11', NULL, 3, TRUE, 5, 6),
('Творческий вечер с друзьями', 'Общение и вдохновение на творчество в кругу близких людей.', '2024-04-10', '12:00:00', 3, NULL, NULL, 1, FALSE, 6, 7),
('Экскурсия в исторический музей', 'Изучение прошлого через экспозиции и артефакты.', '2024-04-11', '13:00:00', 0, NULL, NULL, 4, FALSE, 7, 8),
('Изучение астрономии под открытым небом', 'Наблюдение за звёздами и планетами для познания космоса.', '2024-04-12', '14:00:00', 0, NULL, NULL, 1, FALSE, 8, 9),
('Пикник в парке', 'Наслаждение природой и вкусной едой на свежем воздухе.', '2024-04-13', '15:00:00', 1, '2024-04-16', '15:34:00', 3, TRUE, 9, 10),
('Участие в волонтёрской акции', 'Помощь нуждающимся и улучшение общества через активное участие.', '2024-04-14', '16:00:00', 7, NULL, NULL, 2, FALSE, 11, 11),
('Ролевая игра в стиле фэнтези', 'Погружение в мир волшебства и приключений с помощью воображения.', '2024-04-15', '17:00:00', 5, NULL, NULL, 2, FALSE, 11, 12),
('День заботы о себе', 'SPA и массаж для полного расслабления и оздоровления.', '2024-04-16', '10:20:00', 0, NULL, NULL, 2, FALSE, 10, 13),
('Поход на концерт любимой группы', 'Погружение в атмосферу музыкального праздника и эмоций.', '2024-04-15', '11:00:00', 1, '2024-04-18', '15:00:00', 3, FALSE, 2, 14),
('Разведение растений на балконе', 'Создание зелёного уголка и уход за растениями на балконе.', '2024-04-13', '12:00:00', 0, '2024-04-13', NULL, 1, FALSE, 3, 15),
('Соревнования по настольным играм', 'Захватывающие сражения за победу в любимых настольных играх.', '2024-04-18', '13:00:00', 0, NULL, NULL, 4, FALSE, 5, 2),
('Мастер-класс по изготовлению украшений', 'Создание уникальных и стильных аксессуаров своими руками.', '2024-04-19', '14:00:00', 0, NULL, NULL, 1, FALSE, 2, 3),
('Плавание с дельфинами', 'Встреча и общение с удивительными морскими обитателями.', '2024-04-20', '15:00:00', 21, '2024-04-23', '15:34:00', 3, TRUE, 3, 4),
('Вечер в кинотеатре под открытым небом', 'Просмотр фильмов на свежем воздухе под звёздным небом.', '2024-04-21', '16:00:00', 1, NULL, NULL, 2, FALSE, 11, 5),
('Тур по городским красивым местам', 'Изучение достопримечательностей города и его уникальных мест.', '2024-04-22', '17:00:00', 0, NULL, NULL, 2, FALSE, 2, 6),
('Обучение основам кулинарии', 'Изучение базовых навыков и секретов кулинарии для создания вкусных блюд.', '2024-04-23', '10:20:00', 5, NULL, NULL, 2, FALSE, 2, 7),
('Фотосессия с домашними питомцами', 'Создание весёлых и трогательных фотографий с любимыми животными.', '2024-04-22', '11:00:00', 1, '2024-04-25', NULL, 3, FALSE, 3, 8),
('Участие в квесте по городу', 'Приключение и поиск загадок и секретов города.', '2024-04-24', '12:00:00', 12, NULL, NULL, 1, FALSE, 4, 9),
('Посещение ботанического сада', 'Исследование разнообразия растений и их красоты.', '2024-04-13', '13:00:00', 0, '2024-04-13', '13:30:00', 4, TRUE, 1, 10),
('Занятие йогой на пляже', 'Практика йоги на песчаном пляже с шумом морского прибоя.', '2024-04-26', '14:00:00', 0, NULL, NULL, 1, FALSE, 5, 11),
('Экскурсия на вертолёте', 'Незабываемый полёт на вертолёте с панорамным обзором города.', '2024-04-11', '15:00:00', 1, '2024-04-30', '15:34:00', 3, TRUE, 6, 12),
('Изучение местной культуры и традиций', 'Погружение в обычаи и традиции местного населения для лучшего понимания их жизни.', '2024-04-28', '16:00:00', 1, NULL, NULL, 2, FALSE, 7, 12),
('Марафон просмотра фильмов', 'Наслаждение просмотром фильмов весь день наедине или с друзьями.', '2024-04-29', '17:00:00', 0, NULL, NULL, 2, FALSE, 7, 13),
('Сплав по реке на плотах', 'Путешествие по живописным местам на плотах по реке.', '2024-04-30', '10:20:00', 0, NULL, NULL, 2, FALSE, 8, 14),
('Уроки вождения на мотоцикле', 'Обучение техникам безопасного управления мотоциклом.', '2024-04-29', '11:00:00', 1, '2024-05-02', NULL, 3, FALSE, 9, 15),
('Организация вечера научной фантастики', 'Обсуждение темы научной фантастики с друзьями и коллегами.', '2024-05-01', '12:00:00', 32, NULL, NULL, 1, FALSE, 9, 3),
('Поход на ярмарку рукоделия', 'Покупка уникальных и оригинальных рукодельных изделий на ярмарке.', '2024-05-02', '13:00:00', 0, NULL, NULL, 4, FALSE, 10, 2),
('Обучение танцам под открытым небом', 'Изучение танцевальных движений и ритмов на свежем воздухе.', '2024-05-03', '14:00:00', 32, NULL, NULL, 1, FALSE, 10, 1),
('Встреча со знаменитостью', 'Возможность познакомиться и пообщаться с известным человеком.', '2024-05-04', '15:00:00', 1, '2024-05-07', '15:34:00', 3, FALSE, 3, 3),
('Фотосессия в студии', 'Создание стильных и профессиональных фотографий в студийной обстановке.', '2024-05-05', '16:00:00', 1, NULL, NULL, 2, FALSE, 1, 1),
('Занятия стрельбой из лука', 'Изучение и совершенствование навыков меткости и концентрации при стрельбе из лука.', '2024-05-06', '17:00:00', 12, NULL, NULL, 2, FALSE, 2, 2),
('Уроки катания на вейкборде', 'Обучение управлению вейкбордом на воде.', '2024-05-07', '10:20:00', 0, NULL, NULL, 2, FALSE, 1, 3),
('Оздоровительный курс йоги и медитации', 'Практика йоги и медитации для улучшения здоровья и благополучия.', '2024-05-06', '11:00:00', 1, '2024-05-09', NULL, 3, FALSE, 4, 9),
('Романтический ужин на крыше с видом на город', 'Незабываемый ужин под открытым небом с великолепным видом на городской пейзаж.', '2024-05-08', '12:00:00', 21, NULL, NULL, 1, FALSE, 3, 7),
('Тур по архитектурным достопримечательностям', 'Изучение архитектурных шедевров и исторических мест города.', '2024-05-09', '13:00:00', 22, NULL, NULL, 4, FALSE, 2, 6),
('Мастер-класс по плетению корзин', 'Обучение техникам плетения корзин из натуральных материалов.', '2024-05-10', '14:00:00', 0, NULL, NULL, 1, FALSE, 7, 4),
('Поход на квест-комнату', 'Путешествие в мир загадок и приключений с решением интересных задач.', '2024-05-11', '15:00:00', 4, '2024-05-14', '15:34:00', 3, FALSE, 5, 9),
('Уроки аквапарка', 'Обучение правилам и техникам безопасного плавания и развлечений в аквапарке.', '2024-04-12', '16:00:00', 1, '2024-04-15', NULL, 2, TRUE, 8, 12),
('Посещение мастерской по изготовлению керамики', 'Создание уникальных керамических изделий под руководством опытных мастеров.', '2024-05-13', '17:00:00', 0, NULL, NULL, 2, FALSE, 2, 14),
('День экстримальных видов спорта', 'Занятие экстремальными видами спорта для адреналинового подъёма и новых ощущений.', '2024-05-14', '10:20:00', 32, NULL, NULL, 2, FALSE, 1, 1),
('Медитация в лабиринте', 'Погружение в глубокое состояние релаксации и саморефлексии в лабиринте.', '2024-05-13', '11:00:00', 1, '2024-05-16', NULL, 3, FALSE, 3, 2);



INSERT INTO "Event" ("EventId", "Title", "Description", "StartDate", "StartTime", "EndDate", "EndTime", "Location", "DressCode", "Priority", "CategoryId", "UserId")
VALUES
(1, 'День рождения Ивана', 'Празднование дня рождения Ивана', '2024-05-10', '19:00:00', '2024-05-11', '11:00:00', 'ул. Главная, д. 123', 2, 3, 2, 1),
(2, 'Конференция', 'Посещение технической конференции', '2024-04-20', '9:00:00', '2024-04-22', '18:00:00', 'Конгресс-центр', 1, 2, 1, 14),
(3, 'Семейный пикник', 'Провести выходные на природе с семьей', '2024-05-05', '10:00:00', '2024-05-06', '18:00:00', 'Парк "Лесная поляна"', 5, 3, 2, 3),
(4, 'Концерт рок-группы', 'Посетить концерт любимой рок-группы', '2024-04-19', '20:00:00', '2024-04-20', '21:00:00', 'Клуб "Роксити"', 1, 2, 1, 2),
(5, 'Тренировка в бассейне', 'Посетить тренировку в бассейне', '2024-04-17', '18:30:00','2024-04-17', '23:30:00', 'Городской бассейн', 3, 1, 6, 14),
(6, 'Вечеринка у друзей', 'Провести вечер в компании друзей', '2024-04-26', '19:00:00', '2024-04-27', '13:00:00', 'Ул. Садовая, д. 10', 1, 2, 2, 3),
(7, 'Посещение спа-центра', 'Наслаждение спа-процедурами', '2024-04-21', '15:00:00','2024-04-21', '20:00:00', 'Спа-центр "Оазис"', 6, 3, 3, 8),
(8, 'День рождения Анны', 'Празднование дня рождения Анны', '2024-06-15', '18:00:00', '2024-06-16', '10:00:00', 'Ул. Центральная, д. 45', 1, 3, 2, 2),
(9, 'Конференция по IT', 'Участие в конференции по информационным технологиям', '2024-06-25', '10:00:00', '2024-06-27', '17:00:00', 'Конференц-зал', 3, 2, 1, 1),
(10, 'Поход в горы', 'Провести выходные на природе в горах', '2024-07-05', '8:00:00', '2024-07-06', '18:00:00', 'Горный хребет "Эверест"', 1, 3, 2, 3),
(11, 'Выставка живописи', 'Посетить выставку классической живописи', '2024-05-20', '11:00:00', '2024-05-21', '17:00:00', 'Галерея "Искусство"', 8, 2, 1, 2),
(12, 'Футбольный матч', 'Посещение матча между любимыми командами', '2024-06-01', '16:00:00', '2024-06-01', '18:30:00', 'Стадион "Олимпийский"', 4, 1, 3, 1),
(13, 'Вечеринка выпускников', 'Проведение вечера в кругу одноклассников', '2024-05-31', '20:00:00', '2024-06-01', '2:00:00', 'Клуб "Созвездие"', 1, 2, 6, 3),
(14, 'Посещение театра', 'Посмотреть спектакль в театре', '2024-06-10', '18:30:00', '2024-06-10', '21:00:00', 'Театр "Шекспир"', 5, 3, 3, 7),
(15, 'День рождения Михаила', 'Празднование дня рождения Михаила', '2024-08-20', '16:00:00', '2024-08-21', '12:00:00', 'Ул. Пролетарская, д. 78', 1, 3, 2, 15),
(16, 'Конференция по маркетингу', 'Участие в международной конференции по маркетингу', '2024-07-15', '9:00:00', '2024-07-17', '18:00:00', 'Конференц-центр "Маркет"', 1, 2, 6, 2),
(17, 'Семинар по саморазвитию', 'Участие в семинаре по личностному росту', '2024-08-01', '14:00:00', '2024-08-01', '18:00:00', 'Учебный центр "Гармония"', 6, 3, 5, 12),
(18, 'Выставка современного искусства', 'Посещение выставки современных художников', '2024-07-10', '12:00:00', '2024-07-10', '18:00:00', 'Галерея "Современность"', 1, 2, 4, 2),
(19, 'Концерт классической музыки', 'Посещение концерта классической музыки', '2024-08-05', '19:30:00', '2024-08-05', '22:00:00', 'Филармония им. Чайковского', 4, 1, 3, 1),
(20, 'Встреча старых друзей', 'Встреча с друзьями из детства', '2024-09-01', '17:00:00', '2024-09-01', '23:00:00', 'Кафе "Детство"', 7, 2, 2, 13),
(21, 'Посещение музея', 'Посещение музея истории местного края', '2024-07-29', '11:00:00', '2024-07-29', '15:00:00', 'Музей "Краеведение"', 1, 3, 3, 2),
(22, 'День рождения Елены', 'Празднование дня рождения Елены', '2024-09-10', '18:00:00', '2024-09-11', '10:00:00', 'Ул. Солнечная, д. 7', 8, 3, 2, 1),
(23, 'Конференция по медицине', 'Участие в конференции по современным методам лечения', '2024-10-15', '8:00:00', '2024-10-17', '17:00:00', 'Конференц-зал "Медицина"', 9, 2, 4, 11),
(24, 'Мастер-класс по кулинарии', 'Участие в мастер-классе по приготовлению десертов', '2024-09-20', '14:00:00', '2024-09-20', '18:00:00', 'Школа кулинарии "Вкусняшка"', 1, 3, 2, 3),
(25, 'Выставка автомобилей', 'Посещение автомобильной выставки', '2024-09-25', '10:00:00', '2024-09-25', '18:00:00', 'Автосалон "Автоэкспо"', 1, 2, 1, 2),
(26, 'Тренировка в фитнес-клубе', 'Посещение тренировки в зале фитнеса', '2024-10-01', '18:30:00', '2024-10-01', '20:30:00', 'Фитнес-клуб "Фитнес+"', 1, 1, 3, 1),
(27, 'Вечеринка в стиле ретро', 'Вечеринка в стиле прошлых десятилетий', '2024-09-28', '20:00:00', '2024-09-29', '1:00:00', 'Усадьба "Ретро"', 1, 2, 2, 11),
(28, 'Посещение кинофестиваля', 'Посещение кинофестиваля мирового масштаба', '2024-10-05', '17:00:00', '2024-10-07', '23:00:00', 'Кинотеатр "Глобус"', 2, 3, 3, 2),
(29, 'День рождения', 'Празднование дня рождения Ольги', '2024-11-15', '18:00:00', '2024-11-16', '10:00:00', 'ул. Цветочная, д. 56', 3, 3, 2, 1),
(30, 'Семинар по финансовой грамотности', 'Участие в семинаре по управлению финансами', '2024-10-20', '14:00:00', '2024-10-20', '18:00:00', 'Бизнес-центр "Финансы"', 1, 2, 7, 10),
(31, 'Посещение концерта', 'Посещение концерта любимого исполнителя', '2024-11-05', '19:30:00', '2024-11-05', '22:00:00', 'Зал "Музыкальный"', 3, 1, 1, 3),
(32, 'Вечеринка "Хэллоуин"', 'Вечеринка в стиле Хэллоуин', '2024-10-31', '20:00:00', '2024-11-01', '2:00:00', 'Клуб "Страшилки"', 1, 2, 1, 3),
(33, 'Посещение театрального постановления', 'Посещение театрального спектакля', '2024-12-10', '18:30:00', '2024-12-10', '21:00:00', 'Театр "Актер"', 1, 3, 1, 2),
(34, 'День рождения Дмитрия', 'Празднование дня рождения Дмитрия', '2025-01-20', '16:00:00', '2025-01-21', '12:00:00', 'ул. Пушкина, д. 25', 5, 3, 1, 13),
(35, 'Конференция по бизнесу', 'Участие в конференции по предпринимательству', '2025-01-15', '9:00:00', '2025-01-17', '18:00:00', 'Бизнес-центр "Стартапы"', 1, 2, 6, 2),
(36, 'Поход на водопады', 'Провести выходные на природе у водопадов', '2025-02-05', '8:00:00', '2025-02-06', '18:00:00', 'Водопады "Сказочные"', 5, 3, 1, 12),
(37, 'Выставка фотографий', 'Посещение выставки работ местных фотографов', '2025-01-25', '12:00:00', '2025-01-25', '18:00:00', 'Галерея "ФотоАрт"', 1, 2, 1, 1),
(38, 'Концерт поп-музыки', 'Посещение концерта популярного исполнителя', '2025-02-15', '19:30:00', '2025-02-15', '22:00:00', 'Арена "ПопСцена"', 4, 1, 1, 1),
(39, 'Вечеринка "Рождество"', 'Вечеринка в стиле Рождества', '2024-12-24', '20:00:00', '2024-12-25', '1:00:00', 'Усадьба "Рождественская"', 1, 2, 9, 3),
(40, 'Посещение музыкального фестиваля', 'Посещение фестиваля современной музыки', '2025-03-10', '17:00:00', '2025-03-12', '23:00:00', 'Парк "Музыкальный"', 8, 3, 1, 2),
(41, 'День рождения Александра', 'Празднование дня рождения Александра', '2025-03-15', '18:00:00', '2025-03-16', '10:00:00', 'ул. Центральная, д. 78', 2, 3, 1, 1),
(42, 'Конференция по науке', 'Участие в конференции по современным научным открытиям', '2025-03-25', '9:00:00', '2025-03-27', '17:00:00', 'Научный центр "Знания"', 1, 2, 1, 2),
(43, 'Семинар по самозащите', 'Участие в семинаре по приемам самообороны', '2025-04-01', '14:00:00', '2025-04-01', '18:00:00', 'Дворец спорта "Защита"', 6, 3, 1, 3),
(44, 'Выставка животных', 'Посещение выставки различных видов животных', '2025-04-10', '12:00:00', '2025-04-10', '18:00:00', 'Зоопарк "Веселка"', 1, 2, 1, 4),
(45, 'Концерт джазовой музыки', 'Посещение концерта джазовой музыки', '2025-04-20', '19:30:00', '2025-04-20', '22:00:00', 'Джаз-клуб "Синяя ночь"', 1, 1, 1, 4),
(46, 'Вечеринка "День святого Валентина"', 'Вечеринка в стиле Дня всех влюбленных', '2025-02-14', '20:00:00', '2025-02-15', '2:00:00', 'Ресторан "Влюбленные"', 1, 2, 1, 13),
(47, 'Посещение цирка', 'Посещение представления в цирке', '2025-05-10', '14:00:00', '2025-05-10', '18:00:00', 'Цирк "Смехотун"', 1, 3, 3, 5),
(48, 'День рождения Евгении', 'Празднование дня рождения Евгении', '2025-06-20', '16:00:00', '2025-06-21', '12:00:00', 'ул. Солнечная, д. 45', 1, 3, 2, 15),
(49, 'Конференция по экологии', 'Участие в конференции по проблемам экологии', '2025-05-15', '9:00:00', '2025-05-17', '18:00:00', 'Эко-центр "Зеленый мир"', 1, 2, 4, 2),
(50, 'Поход на озеро', 'Провести выходные на природе у озера', '2025-06-05', '8:00:00', '2025-06-06', '18:00:00', 'Озеро "Синевир"', 1, 3, 1, 14);


INSERT INTO "Participant" ("ParticipantId", "IsAccepted", "Role", "IdEvent", "IdUser")
VALUES
(1, TRUE, 'Организатор', 1, 1),
(2, TRUE, 'Музыкант', 1, 2),
(3, TRUE, 'Диджей', 2, 1),
(4, TRUE, 'Суфлер', 2, 4),
(5, TRUE, 'Менеджер', 3, 8),
(6, TRUE, 'Гость', 4, 2),
(7, TRUE, 'Менеджер', 4, 15),
(8, TRUE, 'Ведущий', 5, 1),
(9, TRUE, 'Гость', 5, 15),
(10, TRUE, 'Диджей', 6, 5),
(11, FALSE, 'Гость', 7, 7),
(12, TRUE, 'Менеджер', 7, 1),
(13, FALSE, 'Организатор', 8, 3),
(14, FALSE, 'Музыкант', 8, 14),
(15, TRUE, 'Гость', 9, 1),
(16, TRUE, 'Менеджер', 9, 3),
(17, FALSE, 'Ведущий', 10, 1),
(18, TRUE, 'Музыкант', 10, 2),
(19, TRUE, 'Гость', 11, 11),
(20, TRUE, 'Менеджер', 11, 2),
(21, TRUE, 'Организатор', 12, 1),
(22, TRUE, 'Суфлер', 12, 3),
(23, TRUE, 'Гость', 13, 2),
(24, TRUE, 'Менеджер', 13, 15),
(25, TRUE, 'Ведущий', 14, 3),
(26, TRUE, 'Музыкант', 14, 2),
(27, FALSE, 'Гость', 15, 1),
(28, TRUE, 'Менеджер', 15, 3),
(29, TRUE, 'Организатор', 16, 2),
(30, TRUE, 'Музыкант', 16, 1),
(31, FALSE, 'Гость', 17, 3),
(32, TRUE, 'Менеджер', 17, 15),
(33, TRUE, 'Организатор', 18, 7),
(34, TRUE, 'Суфлер', 18, 1),
(35, TRUE, 'Гость', 19, 2),
(36, TRUE, 'Менеджер', 19, 3),
(37, TRUE, 'Ведущий', 20, 15),
(38, FALSE, 'Музыкант', 20, 2),
(39, TRUE, 'Гость', 21, 3),
(40, TRUE, 'Менеджер', 21, 1),
(41, TRUE, 'Организатор', 22, 2),
(42, TRUE, 'Суфлер', 22, 3),
(43, TRUE, 'Гость', 23, 14),
(44, TRUE, 'Менеджер', 23, 3),
(45, TRUE, 'Ведущий', 24, 2),
(46, TRUE, 'Музыкант', 24, 1),
(47, TRUE, 'Гость', 25, 3),
(48, FALSE, 'Менеджер', 25, 12),
(49, TRUE, 'Организатор', 26, 1),
(50, TRUE, 'Суфлер', 26, 8),
(51, TRUE, 'Организатор', 27, 12),
(52, FALSE, 'Музыкант', 27, 1),
(53, TRUE, 'Гость', 28, 3),
(54, TRUE, 'Менеджер', 28, 2),
(55, TRUE, 'Организатор', 29, 9),
(56, TRUE, 'Суфлер', 29, 13),
(57, TRUE, 'Гость', 30, 2),
(58, TRUE, 'Менеджер', 30, 3),
(59, FALSE, 'Ведущий', 31, 9),
(60, TRUE, 'Музыкант', 31, 2),
(61, TRUE, 'Гость', 32, 3),
(62, TRUE, 'Менеджер', 32, 11),
(63, FALSE, 'Организатор', 33, 3),
(64, TRUE, 'Суфлер', 33, 11),
(65, TRUE, 'Гость', 34, 2),
(66, TRUE, 'Менеджер', 34, 3),
(67, FALSE, 'Ведущий', 35, 1),
(68, TRUE, 'Музыкант', 35, 2),
(69, FALSE, 'Гость', 36, 9),
(70, TRUE, 'Менеджер', 36, 1),
(71, TRUE, 'Организатор', 37, 8),
(72, TRUE, 'Суфлер', 37, 3),
(73, TRUE, 'Гость', 38, 4),
(74, TRUE, 'Менеджер', 38, 9),
(75, FALSE, 'Ведущий', 39, 2),
(76, TRUE, 'Музыкант', 39, 12),
(77, TRUE, 'Гость', 40, 3),
(78, TRUE, 'Менеджер', 40, 2),
(79, TRUE, 'Организатор', 41, 1),
(80, TRUE, 'Суфлер', 41, 11),
(81, TRUE, 'Гость', 42, 2),
(82, FALSE, 'Менеджер', 42, 13),
(83, TRUE, 'Организатор', 43, 5),
(84, TRUE, 'Музыкант', 43, 1),
(85, FALSE, 'Гость', 44, 14),
(86, TRUE, 'Менеджер', 44, 3),
(87, TRUE, 'Ведущий', 45, 1),
(88, FALSE, 'Музыкант', 45, 10),
(89, TRUE, 'Гость', 46, 3),
(90, FALSE, 'Менеджер', 46, 1),
(91, TRUE, 'Организатор', 47, 12),
(92, TRUE, 'Суфлер', 47, 3),
(93, FALSE, 'Гость', 48, 14),
(94, TRUE, 'Менеджер', 48, 3),
(95, FALSE, 'Ведущий', 49, 2),
(96, TRUE, 'Музыкант', 49, 13),
(97, TRUE, 'Гость', 50, 3),
(98, TRUE, 'Менеджер', 50, 2),
(99, TRUE, 'Организатор', 50, 15),
(100, TRUE, 'Суфлер', 50, 3);