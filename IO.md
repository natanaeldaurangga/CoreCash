# Cash

## Insert New Cash Record

### Header

- Endpoint: api/Cash
- Type: POST
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
- Type: DELETE
- Accept: application/json
- Authorization: Bearer [token] (User)

### Response

- 204 No Content

## Get Cash

### Header

- Endpoint: api/Cash
- Type: GET
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
      "userId": "uuid",
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
- Type: GET
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
      "deskripsi": "lorem ipsum dolor sit amet."
    }
}
```

# Contact

## Insert New Contact

### Header

- Endpoint: api/Contact
- Type: GET
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
- Type: DELETE
- Accept: application/json

### Response

- 204 No Content

## Get Contact

### Header

- Endpoint: api/Contact
- Type: DELETE
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
