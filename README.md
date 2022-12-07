
# Microservices

This is a simple microservices project .

## Services

- API Gateway Service
- Contact Services
- Report Services

### Tech Stack
- .Net Core
- PostgreSQL
- RabbitMQ

## Run Locally

Clone the project

```bash
  git clone https://github.com/berkSahin/rise.git
```

Go to the each service directory and build docker image. 

```bash
  docker build -t <Image_name> .
```

Run docker compose

```bash
  docker-compose up -d
```
## API Gateway Endpoints via Postman

If you import 'Rise.postman_collection.json' file to Postman, you can use prepared requests collection.

## API Gateway Endpoints

#### Add Contact

```bash
  POST /api/AddContact
```
| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `contact`      | `ContactDTO` | **Required** |

#### Delete Contact

```bash
  DELETE /api/DeleteContact/{contactId}
```
| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `contactId`      | `int` | **Required** |

#### Get all contacts

```bash
  GET /api/GetContacts
```

#### Get Contact by Contact Id

```bash
  GET /api/GetContact/{contactId}
```
| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `contactId`      | `int` | **Required** |

#### Get all Contact with Infos

```bash
  GET /api/GetContactsWithInfos
```

#### Get Contact with Infos by Contact Id

```bash
  GET /api/GetContactWithInfos/{contactId}
```
| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `contactId`      | `int` | **Required** |

#### Add Contact Info

```bash
  POST /api/AddContactInfo
```
| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `contactInfo`      | `ContactInfoDTO` | **Required** |

#### Delete Contact Info by Contact Info Id

```bash
  DELETE /api/DeleteContactInfo/{contactInfoId}
```
| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `contactInfoId`      | `int` | **Required** |

#### Get Contact Info Types

```bash
  GET /api/GetContactInfoTypes
```

#### Get Contact Infos by Contact Id

```bash
  GET /api/GetContactInfosByContactId/{contactId}
```
| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `contactId`      | `int` | **Required** |

#### Create Report

```bash
  GET /api/CreateReport
```

#### Get all Reports

```bash
  GET /api/GetReports
```

#### Get Report Details by Report Id

```bash
  GET /api/GetReportDetail/{reportId}
```
| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `reportId`      | `int` | **Required** |

#### Download Report by Path

```bash
  GET /api/DownloadReport/{path}
```
| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `path`      | `string` | **Required** |