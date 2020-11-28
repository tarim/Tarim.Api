CREATE DEFINER=`root`@`localhost` PROCEDURE `ADD_PROVERB`(IN content_in VARCHAR(512),
 IN category_in ENUM('Proverb','Idiom','Wisdom','Other'),
 IN description_in VARCHAR(512),IN user_recid_in INT, OUT id_out INT)
BEGIN
INSERT INTO tbl_uyghur_proverb(proverb,category,description,user_recid) 
values(content_in,category_in,description_in,user_recid_in);

SET id_out=(SELECT LAST_INSERT_ID());
END