INSERT INTO "locality" ("name") VALUES
('Тюмень'),
('Тюнево'),
('Сорокино'),
('Ялуторовск');

INSERT INTO "type_organization" ("name") VALUES
('Значения справочника'),
('Исполнительный орган государственной власти'),
('Орган местного самоуправления'),
('Организация по отлову'),
('Организация по отлову и приют'),
('Организация по транспортировке'),
('Ветеринарная клиника: государственная'),
('Ветеринарная клиника: муниципальная'),
('Ветеринарная клиника: частная'),
('Благотворительный фонд'),
('Организации по продаже товаров и предоставлению услуг для животных'),
('Заявитель (для регистрации представителя юридического лица, подающего заявку на отлов)');

INSERT INTO "legal_type" ("name") VALUES
('Индивидуальный предприниматель'),
('Юридическое лицо');

INSERT INTO "organization" 
(name_organization, inn, kpp, "address", type_organization_id, legal_type_id, locality_id) 
VALUES
('ООО "Альфа"', '123456789', '123456789', 'ул. Ленина, д. 1', 1, 1, 1),
('ООО "Бета"', '987654321', '987654321', 'ул. Пушкина, д. 1', 1, 2, 2),
('ИП "Гамма"', '135792468', null, 'ул. Гагарина, д. 1', 2, 1, 3),
('ОАО "Дельта"', '246813579', '246813579', 'ул. Ленина, д. 1', 3, 2, 4),
('ЗАО "Эпсилон"', '369258147', '369258147', 'ул. Пушкина, д. 1', 4, 1, 2),
('ООО "Сигма"', '159753486', '159753486', 'ул. Ленина, д. 1', 1, 2, 4),
('ИП "Фи', '258963147', null, 'ул. Пушкина, д. 1', 2, 1, 1),
('ОАО "Хи"', '753951486', '753951486', 'ул. Ленина, д. 1', 3, 2, 2),
('ЗАО "Ци"', '951357864', '951357864', 'ул. Пушкина, д. 1', 4, 1, 3),
('ООО "Ши"', '456789123', '456789123', 'ул. Ленина, д. 1', 1, 2, 4);

INSERT INTO "contract"
("number", date_of_conclusion, date_valid, executor_id, client_id)
VALUES
('12345', '2021-01-01', '2022-01-01', 1, 1),
('54321', '2021-02-01', '2022-02-01', 2, 3),
('67890', '2021-03-01', '2022-03-01', 3, 7);

INSERT INTO "contract_content"
(price, contract_id, locality_id)
VALUES
(100, 1, 1),
(200, 1, 3),
(150, 2, 2);

INSERT INTO "act"
(executor_id, locality_id, date_of_capture, contract_id)
VALUES
(1, 1, '2021-02-01', 1),
(2, 2, '2021-03-01', 2);

INSERT INTO "animal"
(category, sex, breed, size, wool, color, ears, tail, special_signs, identification_label, chip_number, act_id)
VALUES
('Кошка', true, 'Сиамская', 3.5, 'Короткая', 'Коричневый', 'Прямые', 'Длинный', 'Отсутствуют', '123456789', 'ABC123', 1),
('Собака', false, 'Лабрадор', 25.2, 'Длинная', 'Черный', 'Висячие', 'Короткий', 'Большой рубец на животе', '987654321', 'XYZ789', 1),
('Кошка', true, 'Персидская', 4.2, 'Длинная', 'Белый', 'Прямые', 'Длинный', 'Отсутствуют', '456789123', 'DEF456', 2),
('Собака', true, 'Овчарка', 35.7, 'Короткая', 'Рыжий', 'Висячие', 'Длинный', 'Отсутствуют', '321654987', 'GHI789', 2),
('Кошка', false, 'Британская', 5.1, 'Короткая', 'Серый', 'Прямые', 'Короткий', 'Отсутствуют', '987321654', 'JKL789', 1);

--создаем суперпользователя
INSERT INTO "role"
("name")
VALUES('super-man');

INSERT INTO "user"
("login", "password", locality_id, organization_id, role_id)
VALUES('super', 'AQAAAAIAAYagAAAAEO/sYj4RkmFNwdqOe88+1EZEXC6s3BlUOC2kdjT4ZmPxHBMyWUWRF7SKQ8LzhZunIQ==', 1, 1, 1);

INSERT INTO entity_possibilities
(entity, possibility, restriction, role_id)
VALUES
('Authorization', 'Read', 'All', 1),
('Authorization', 'Insert', 'All', 1),
('Authorization', 'Update', 'All', 1),
('Authorization', 'Delete', 'All', 1),
('Organization', 'Read', 'All', 1),
('Organization', 'Insert', 'All', 1),
('Organization', 'Update', 'All', 1),
('Organization', 'Delete', 'All', 1),
('Contract', 'Read', 'All', 1),
('Contract', 'Insert', 'All', 1),
('Contract', 'Update', 'All', 1),
('Contract', 'Delete', 'All', 1);


-- TODO сделать роли и пользователей хотя бы пару штук!!!