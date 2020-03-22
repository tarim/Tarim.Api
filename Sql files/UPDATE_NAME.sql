CREATE DEFINER=`root`@`localhost` PROCEDURE `UPDATE_NAME`(IN id_in INT,IN name_ug_in VARCHAR(64),IN name_latin_in VARCHAR(64),IN origination_in ENUM('Uyghur', 'Arabic', 'Persian', 'Other'),
 IN gender_in ENUM('male', 'female', 'unisex'),IN related_name_in VARCHAR(64),IN is_surname_in TINYINT,IN description_in VARCHAR(256))
BEGIN
UPDATE tbl_uyghur_name SET name_ug=coalesce(name_ug_in,name_ug),name_latin=coalesce(name_latin_in,name_latin),
origination=coalesce(origination_in,origination),gender=coalesce(gender_in,gender),
related_name=coalesce(related_name_in,related_name),is_surname=coalesce(is_surname_in,is_surname),
`description`=coalesce(description_in,`description`) WHERE id=id_in;
END