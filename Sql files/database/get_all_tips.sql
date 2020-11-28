CREATE DEFINER=`root`@`localhost` PROCEDURE `GET_ALL_TIPS`()
BEGIN
SELECT T.recid id,T.title,T.summary,T.category,T.source,U.first_name,T.private from tbl_tips AS T
 JOIN tbl_user AS U ON T.user_recid=U.recid WHERE T.status='Active' ORDER BY id desc;
END