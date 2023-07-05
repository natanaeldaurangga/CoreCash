# Auth

## User Register

### Header

- Endpoint: api/Auth/Registration
- Method: POST
- Accept: multipart/form-data
- Authorization: Bearer [token] (User)

### Request Body

```
Name: "string"
Email:  "string"
Password: "string"
ProfilePicture: "image/png || image/jpg"
```

## Login

### Header

- Endpoint: api/Auth/Registration
- Method: POST
- Accept: application/json
- Authorization: Bearer [token] (User/Admin)

### Request Body

```json
{
  "email": "string",
  "password": "string"
}
```

### Response Body

- 200 Ok
  ```json
  {
    "name": "string",
    "role": "string",
    "email": "string",
    "jwt_token": "string"
  }
  ```

### Header

# Cash

## Insert New Cash Record (Cash In/Out)

### Header

- Endpoint: api/Cash
- Method: POST
- Accept: application/json
- Authorization: Bearer [token] (User)

### Request Body

```json
{
  "transaction_date": "2023-07-04T14:27:00",
  "description": "lorem ipsum dolor sit amet.",
  "balance": 0
}
```

### Response

- 200 Success
  ```json
  {
    "messsage": "data berhasil ditambahkan"
  }
  ```

## Delete Cash

### Header

- Endpoint: api/Cash/{recordId}
- Method: DELETE
- Accept: application/json
- Authorization: Bearer [token] (User)

### Response

- 204 No Content

## Get Cash

### Header

- Endpoint: api/Cash
- Method: GET
- Accept: application/json
- Authorization: Bearer [token] (User)

### Request Query

```
api/Cash?Limit=5&CurrentPage=1&OrderBy=Column&Direction=ASC&Keyword=Test
```

- Limit = 5
- CurrentPage = 1
- OrderBy = "Column"
- Direction = "ASC"
- Keyword = "Test"

### Response Body

```json
{
  "totalRowsInTable": 100,
  "currentPage": 1,
  "pageSize": 5,
  "totalPages": 20,
  "items": [
    {
      "recordId": "uuid",
      "transaction_date": "2023-07-07T15:08.000Z",
      "entry": "DEBIT",
      "balance": 1000000
    }
  ]
}
```

## Get Detail Cash Record

### Header

- Endpoint: api/Cash/{recordId}
- Method: GET
- Accept: application/json
- Authorization: Bearer [token] (User)

### Response Body

```json
{
    {
      "recordId": "uuid",
      "userId": "uuid",
      "transaction_date": "2023-07-07T15:08.000Z",
      "entry": "DEBIT",
      "balance": 1000000,
      "description": "lorem ipsum dolor sit amet."
    }
}
```

# Contact

## Insert New Contact

### Header

- Endpoint: api/Contact
- Method: GET
- Accept: application/json
- Authorization: Bearer [token] (User)

### Request Body

```json
{
  "name": "Bento",
  "phone_number": "022-92834323",
  "address": "Jl"
}
```

### Response

- 200 Success

## Soft Delete Contact

### Header

- Endpoint: api/Contact/{contactId}
- Method: DELETE
- Accept: application/json

### Response

- 204 No Content

## Get Contact

### Header

- Endpoint: api/Contact
- Method: GET
- Accept: application/json

### Request Query

```
api/Cash?Limit=5&CurrentPage=1&OrderBy=Column&Direction=ASC&Keyword=Test
```

- Limit = 5
- CurrentPage = 1
- OrderBy = "Column"
- Direction = "ASC"
- Keyword = "Test"

### Response Body

```json
{
  "totalRowsInTable": 100,
  "currentPage": 1,
  "pageSize": 5,
  "totalPages": 20,
  "items": [
    {
      "contactId": "uuid",
      "name": "nama_lengkap",
      "phone_number": "022-19823423",
      "address": "jl. telugu"
    }
  ]
}
```

## Get Detail Contact

### Header

- Endpoint: api/Contact/{id}
- Method: GET
- Accept: application/json
- Authorization: Bearer [token] (User)

### Response Body

```json
{
  "contactId": "uuid",
  "name": "nama_lengkap",
  "phone_number": "022-19823423",
  "address": "jl. telugu"
}
```

# Receivables

## Insert New Receivables

### Header

- Endpoint: api/Receivable
- Method: POST
- Accept: application/json
- Authorization: Bearer [token] (User)

### Request Body

```json
{
  "transaction_date": "2023-07-04T14:27:00",
  "description": "lorem ipsum dolor sit amet.",
  "balance": 0,
  "debtor_id": "uuid"
}
```

### Response

- 200 Success

## Receivables Payment

### Header

- Endpoint: api/Receivable/Payment
- Method: POST
- Accept: application/json
- Authorization: Bearer [token] (User)

### Request Body

```json
{
  "transaction_date": "2023-07-04T14:27:00",
  "description": "lorem ipsum dolor sit amet.",
  "debtor_id": "uuid",
  "balance": 0
}
```

### Response Body

- 200 Ok

## Softdelete Receivables

### Header

- Endpoint: api/Receivable/{recordId}
- Method: DELETE
- Accept: application/json
- Authorization: Bearer [token] (User)

### Response Date

- 200 Ok

## Bad Debt Record

### Header

- Endpoint: api/Receivable/BadDebt/{recordId}
- Method: DELETE
- Accept: application/json
- Authorization: Bearer [token] (User)

### Response Body

- 200 Ok

## Get Receivables

### Header

- Endpoint: api/Receivable
- Method: GET
- Accept: application/json
- Authorization: Bearer [token] (User)

### Response Body

```json
{
  "totalRowsInTable": 100,
  "currentPage": 1,
  "pageSize": 5,
  "totalPages": 20,
  "items": [
    {
      "receivableId": "uuid",
      "recordId": "uuid",
      "record_datetime": "datetime",
      "debtor_id": "uuid",
      "debtor_name": "string",
      "balance": 0
    }
  ]
}
```

## Get Receivable Detail

### Header

- Endpoint: api/Receivable/{recordDetail}
- Method: GET
- Accept: application/json
- Authorization: Bearer [token] (User)

### Response Body

```json
{
  "receivableId": "uuid",
  "debtor_id": "uuid",
  "debtor_name": "string",
  "ledger": [
    {
      "recordId": "uuid",
      "transaction_date": "2023-07-07T15:08.000Z",
      "entry": "debit/credit",
      "balance": "number"
    }
  ]
}
```
