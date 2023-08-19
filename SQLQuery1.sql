USE CoreCashDB;

EXEC sp_help 'dbo.receivable_ledgers';

EXEC sp_help 'dbo.users';

SELECT * FROM records;

SELECT * FROM users;

SELECT * FROM accounts;

SELECT * FROM records;
SELECT * FROM ledgers;

SELECT * FROM contacts;

SELECT SUM(IIF(lg.entry = 1, lg.balance, -lg.balance)) FROM ledgers lg
LEFT JOIN records rc ON rc.id = lg.record_id
GROUP BY rc.user_id;

SELECT * FROM records WHERE record_group = 4;
SELECT * FROM records WHERE id = '19A05C8B-4250-4F88-9C94-A69AE894C657';
UPDATE records SET deleted_at = NULL WHERE id = '19A05C8B-4250-4F88-9C94-A69AE894C657';

SELECT * FROM ledgers WHERE record_id = 'DB4F56AA-2381-4188-9BD9-79E79182E689';

-- DELETE FROM users;

-- DELETE FROM ledgers;
-- DELETE FROM records;
