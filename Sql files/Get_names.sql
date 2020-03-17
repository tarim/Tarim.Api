CREATE DEFINER=`root`@`localhost` PROCEDURE `GET_NAMES`()
BEGIN
SELECT recid,name_ug,name_cn,name_latin,related_name,gender,origination,is_surname,description
 from tbl_uyghur_name WHERE status='Active';
END