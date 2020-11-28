CREATE DEFINER=`root`@`localhost` PROCEDURE `GET_TIP`(IN recid_in INT)
BEGIN
SELECT T.recid id,T.title,T.summary,T.category,T.content,T.source,U.first_name from tbl_tips AS T
 JOIN tbl_user AS U ON T.user_recid=U.recid WHERE T.status='Active' AND T.recid=recid_in;
END