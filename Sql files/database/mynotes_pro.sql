CREATE DEFINER=`root`@`localhost` PROCEDURE `MyNotes`()
BEGIN
SELECT recid id,title,summary,category,content,source,user_name from tbl_note WHERE status='Active';
END