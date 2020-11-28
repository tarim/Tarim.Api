CREATE DEFINER=`root`@`localhost` FUNCTION `GET_ACTION_COUNT`(action_in VARCHAR(20)) RETURNS int
    DETERMINISTIC
BEGIN
declare cnt INT;
select count(*) INTO cnt from tbl_name_action WHERE `action`=action_in;
RETURN cnt;
END