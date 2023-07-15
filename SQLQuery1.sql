USE CoreCashDB;

EXEC sp_help 'dbo.contacts';

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

-- DELETE FROM users;

-- DELETE FROM ledgers;
-- DELETE FROM records;

