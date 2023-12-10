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




INSERT INTO "status"
(status_name)
VALUES
('Не действует'),
('В исполнении'),
('Исполнен'),
('Истёк без отлова');






--create trigger update_plan_status_trigger before
--insert
--    or
--update
--    on
--    public.plan for each row execute function update_plan_status();


--CREATE OR REPLACE FUNCTION public.update_plan_status()
-- RETURNS trigger
-- LANGUAGE plpgsql
--AS $function$
--BEGIN
--    -- Проверяем, если срок исполнения плана графика ещё не настал, то статус меняем на "Не действует"
--    IF NEW."year" > EXTRACT(YEAR FROM CURRENT_DATE) OR (NEW."year" = EXTRACT(YEAR FROM CURRENT_DATE) AND NEW."month" > EXTRACT(MONTH FROM CURRENT_DATE)) THEN
--        NEW.status_id = 1;
--    -- Проверяем, если наступил срок действия плана графика, то статус меняем на "В исполнении"
--    ELSIF NEW."year" = EXTRACT(YEAR FROM CURRENT_DATE) AND NEW."month" = EXTRACT(MONTH FROM CURRENT_DATE) THEN
--        NEW.status_id = 2;
--    -- Проверяем, если срок действия плана-графика истёк и по нему есть акты отлова, то статус меняем на "Исполнен"
--    ELSIF NEW."year" < EXTRACT(YEAR FROM CURRENT_DATE) OR (NEW."year" = EXTRACT(YEAR FROM CURRENT_DATE) AND NEW."month" < EXTRACT(MONTH FROM CURRENT_DATE)) THEN
--        IF EXISTS (
--        	SELECT * FROM plan p 
--        	join plan_content pc on p.id = pc.plan_id 
--        	where pc.act_id notnull and pc.plan_id = new."id") THEN
--            NEW.status_id = 3;
--        ELSE
--            -- Если по плану-графику нет актов отлова, то статус меняем на "Истёк без отлова"
--            NEW.status_id = 4;
--        END IF;
--    END IF;
    
--    RETURN NEW;
--END;
--$function$;


--create trigger update_plan_content_status_trigger after
--insert
--    or
--update
--    on
--    public.plan_content for each row execute function update_plan_content_status();


--CREATE OR REPLACE FUNCTION public.update_plan_content_status()
-- RETURNS trigger
-- LANGUAGE plpgsql
--AS $function$
--declare 
--	plan_year int4;
--	plan_month int4;
--begin	
--	select p."month", p."year" into plan_year, plan_month from public.plan p where p.id = new."plan_id";
--    -- Проверяем, если срок исполнения плана графика ещё не настал, то статус меняем на "Не действует"
--    IF plan_year > EXTRACT(YEAR FROM CURRENT_DATE) OR (plan_year = EXTRACT(YEAR FROM CURRENT_DATE) AND plan_month > EXTRACT(MONTH FROM CURRENT_DATE)) THEN
--        UPDATE public.plan
--		SET status_id=1
--		WHERE id=new."plan_id";
--    -- Проверяем, если наступил срок действия плана графика, то статус меняем на "В исполнении"
--    ELSIF plan_year = EXTRACT(YEAR FROM CURRENT_DATE) AND plan_month = EXTRACT(MONTH FROM CURRENT_DATE) THEN
--        UPDATE public.plan
--		SET status_id=2
--		WHERE id=new."plan_id";
--    -- Проверяем, если срок действия плана-графика истёк и по нему есть акты отлова, то статус меняем на "Исполнен"
--    ELSIF plan_year < EXTRACT(YEAR FROM CURRENT_DATE) OR (plan_year = EXTRACT(YEAR FROM CURRENT_DATE) AND plan_month < EXTRACT(MONTH FROM CURRENT_DATE)) THEN
--        IF EXISTS (SELECT * FROM plan p join plan_content pc on p.id = pc.plan_id where pc.act_id notnull and pc.plan_id = new."plan_id") THEN
--            UPDATE public.plan
--		SET status_id=3
--		WHERE id=new."plan_id";
--        ELSE
--            -- Если по плану-графику нет актов отлова, то статус меняем на "Истёк без отлова"
--            UPDATE public.plan
--			SET status_id=4
--			WHERE id=new."plan_id";
--        END IF;
--    END IF;
    
--    RETURN NEW;
--END;
--$function$
--;
