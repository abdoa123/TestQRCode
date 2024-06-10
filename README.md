## Generate QR Code Endpoint

This .NET Core API provides an endpoint to generate a QR code with a specified `deskNumber`. The QR code can optionally include a logo.

### Endpoint

`GET /GenerateQRCode`

### Query Parameters

- `deskNumber` (required): The desk number to be encoded into the QR code.
- `userName` (optional): The user name, if needed.
- `Email` (optional): The email address, if needed.

### Response

- `200 OK`: The QR code is successfully generated.
- `400 Bad Request`: The `deskNumber` parameter is required and cannot be null or empty.

### Example Request

```http
GET /GenerateQRCode?deskNumber=1234&userName=JohnDoe&Email=johndoe@example.com
