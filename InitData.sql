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
('ООО "Альфа"', '123456789', '123456789', 'г. Москва, ул. Ленина, д. 1', 1, 1, 1),
('ООО "Бета"', '987654321', '987654321', 'г. Санкт-Петербург, ул. Пушкина, д. 1', 1, 2, 2),
('ИП "Гамма"', '135792468', null, 'г. Екатеринбург, ул. Гагарина, д. 1', 2, 1, 3),
('ОАО "Дельта"', '246813579', '246813579', 'г. Новосибирск, ул. Ленина, д. 1', 3, 2, 4),
('ЗАО "Эпсилон"', '369258147', '369258147', 'г. Казань, ул. Пушкина, д. 1', 4, 1, 2),
('ООО "Сигма"', '159753486', '159753486', 'г. Ростов-на-Дону, ул. Ленина, д. 1', 1, 2, 4),
('ИП "Фи', '258963147', null, 'г. Самара, ул. Пушкина, д. 1', 2, 1, 1),
('ОАО "Хи"', '753951486', '753951486', 'г. Уфа, ул. Ленина, д. 1', 3, 2, 2),
('ЗАО "Ци"', '951357864', '951357864', 'г. Волгоград, ул. Пушкина, д. 1', 4, 1, 3),
('ООО "Ши"', '456789123', '456789123', 'г. Нижний Новгород, ул. Ленина, д. 1', 1, 2, 4);


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
