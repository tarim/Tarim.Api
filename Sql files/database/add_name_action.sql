CREATE DEFINER=`root`@`localhost` PROCEDURE `ADD_NAME_ACTION`(IN name_recid_in INT,IN action_in ENUM('LIKE','LOVE','MYNAME'),IN user_ip_in VARCHAR(30),OUT id_out INT)
BEGIN
INSERT INTO tbl_name_action(name_recid,`action`,user_ip) 
values(name_recid_in,action_in,user_ip_in);
SET id_out=(SELECT LAST_INSERT_ID());
END