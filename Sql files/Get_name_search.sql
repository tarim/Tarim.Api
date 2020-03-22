CREATE DEFINER=`root`@`localhost` PROCEDURE `GET_NAME`(IN name_latin_in VARCHAR(64))
BEGIN
SELECT recid,name_ug,name_latin,related_name,gender,origination,is_surname,description
 from tbl_uyghur_name WHERE status='Active' AND name_latin=name_latin_in;
END