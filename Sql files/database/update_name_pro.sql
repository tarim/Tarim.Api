CREATE PROCEDURE `UPDATE_NAME`(IN id_in INT,IN name_ug_in VARCHAR(64),IN name_latin_in VARCHAR(64),IN origination_in ENUM('Uyghur', 'Arabic', 'Persian', 'Other'),
 IN gender_in ENUM('male', 'female', 'unisex'),IN related_name_in VARCHAR(64),IN is_surname_in TINYINT,IN description_in VARCHAR(256))
BEGIN
UPDATE tbl_uyghur_name SET name_ug=IFNULL(name_ug_in,name_ug,name_ug_in),name_latin=IFNULL(name_latin_in,name_latin,name_latin_in),
origination=IFNULL(origination_in,origination,origination_in),gender=IFNULL(gender_in,gender,gender_in),
related_name=IFNULL(related_name_in,related_name,related_name_in),is_surname=IFNULL(is_surname_in,is_surname,is_surname_in),
`description`=IFNULL(description_in,`description`,description_in) WHERE id=id_in;
END