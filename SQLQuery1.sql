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

SELECT rcv.id as receivable_id,
	   sum_table.record_id as record_id,
	   rc.recorded_at as transaction_date,
	   ct.id as debtor_id,
	   ct.name as debtor_name,
	   sum_table.total_balance as balance
FROM receivables rcv
LEFT JOIN contacts ct ON ct.id = rcv.debtor_id
LEFT JOIN records rc ON rc.id = rcv.record_id
LEFT JOIN (
	SELECT rl.receivable_id as receivable_id, rl.record_id as record_id, SUM(IIF(lg.entry = 0, lg.balance, -lg.balance)) as total_balance
	FROM ledgers lg
	JOIN receivable_ledger rl on lg.record_id = rl.record_id
	GROUP BY rl.receivable_id
) sum_table ON sum_table.record_id = rcv.record_id;

-- DELETE FROM users;

-- DELETE FROM ledgers;
-- DELETE FROM records;

