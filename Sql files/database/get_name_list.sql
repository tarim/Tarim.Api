CREATE DEFINER=`root`@`localhost` PROCEDURE `GET_NAME_LIST`(IN pageNumber_in INT)
BEGIN
declare pageNumber INT;
SET pageNumber=(pageNumber_in-1)*200;

SELECT recid,name_ug as nameUg,CONCAT(UCASE(LEFT(name_latin, 1)), SUBSTRING(name_latin, 2)) as nameLatin,
IFNULL(related_name,'') as relatedName,
gender,origin,is_surname,`description` 
FROM tbl_uyghur_name WHERE status='Active' ORDER BY name_ug ASC LIMIT pageNumber, 200;
END