CREATE DEFINER=`root`@`localhost` PROCEDURE `ADD_NAME`(IN name_ug_in VARCHAR(64),IN name_latin_in VARCHAR(64),IN origin_in ENUM('Uyghur', 'Arabic', 'Persian', 'Other'),
 IN gender_in ENUM('male', 'female', 'unisex'),IN related_name_in VARCHAR(64),IN is_surname_in TINYINT,IN description_in VARCHAR(256), OUT id_out INT)
BEGIN
INSERT INTO tbl_uyghur_name(name_ug,name_latin,origin,gender,related_name,is_surname,`description`) 
values(name_ug_in,name_latin_in,origin_in,gender_in,related_name_in,is_surname_in,description_in);

SET id_out=(SELECT LAST_INSERT_ID());
END