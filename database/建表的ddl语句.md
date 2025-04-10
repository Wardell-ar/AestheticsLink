## DDL语句

#### BILL

```sql
CREATE TABLE "SYSTEM"."BILL" 
   (	"BILL_ID" VARCHAR2(20) NOT NULL ENABLE, 
	"CUS_ID" VARCHAR2(20) NOT NULL ENABLE, 
	"COU_ID" VARCHAR2(20), 
	"FOUND_DATE" DATE NOT NULL ENABLE, 
	"PAID_AMOUNT" NUMBER(*,2) NOT NULL ENABLE, 
	"HOS_ID" VARCHAR2(20), 
	 CHECK (PAID_AMOUNT >= 0) ENABLE, 
	 CONSTRAINT "BILL_PK" PRIMARY KEY ("BILL_ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM"  ENABLE, 
	 CONSTRAINT "BILL_COUPON_COU_ID_FK" FOREIGN KEY ("COU_ID")
	  REFERENCES "SYSTEM"."COUPON" ("COU_ID") ENABLE, 
	 CONSTRAINT "BILL_CUSTOMER_CUS_ID_FK" FOREIGN KEY ("CUS_ID")
	  REFERENCES "SYSTEM"."CUSTOMER" ("CUS_ID") ENABLE, 
	 CONSTRAINT "BILL_HOS" FOREIGN KEY ("HOS_ID")
	  REFERENCES "SYSTEM"."HOSPITAL" ("HOS_ID") ENABLE
   ) PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 
 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
```

#### COUPON

```sql
CREATE TABLE "SYSTEM"."COUPON" 
   (	"COU_ID" VARCHAR2(20) NOT NULL ENABLE, 
	"NAME" VARCHAR2(100) NOT NULL ENABLE, 
	"TYPE" VARCHAR2(50) NOT NULL ENABLE, 
	"PRICE" NUMBER(*,2) NOT NULL ENABLE, 
	 CHECK (PRICE>=0) ENABLE, 
	 CONSTRAINT "COUPON_PK" PRIMARY KEY ("COU_ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM"  ENABLE
   ) PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 
 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
```

#### CUSTOMER

```sql
CREATE TABLE "SYSTEM"."CUSTOMER" 
   (	"CUS_ID" VARCHAR2(20) NOT NULL ENABLE, 
	"PHONE_NUM" VARCHAR2(20) NOT NULL ENABLE, 
	"FOUND_DATE" DATE NOT NULL ENABLE, 
	"BIRTHDAY" DATE NOT NULL ENABLE, 
	"GENDER" VARCHAR2(10) NOT NULL ENABLE, 
	"NAME" VARCHAR2(100) NOT NULL ENABLE, 
	"PASSWORD" VARCHAR2(20) NOT NULL ENABLE, 
	"BALANCE" NUMBER(*,2) NOT NULL ENABLE, 
	"VIPLEVEL" VARCHAR2(20) NOT NULL ENABLE, 
	 CONSTRAINT "CUSTOMER_PK" PRIMARY KEY ("CUS_ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM"  ENABLE, 
	 CONSTRAINT "CUSTOMER_MEMBER_VIPLEVEL_FK" FOREIGN KEY ("VIPLEVEL")
	  REFERENCES "SYSTEM"."MEMBER" ("VIPLEVEL") ENABLE
   ) PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 
 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
```

#### CUS_COU

```sql
CREATE TABLE "SYSTEM"."CUS_COU" 
   (	"CUS_ID" VARCHAR2(20) NOT NULL ENABLE, 
	"COU_ID" VARCHAR2(20) NOT NULL ENABLE, 
	 CONSTRAINT "CUSTOMER_COUPON_CUS_ID_FK_R" FOREIGN KEY ("CUS_ID")
	  REFERENCES "SYSTEM"."CUSTOMER" ("CUS_ID") ENABLE, 
	 CONSTRAINT "CUSTOMER_COUPON_COU_ID_FK_R" FOREIGN KEY ("COU_ID")
	  REFERENCES "SYSTEM"."COUPON" ("COU_ID") ENABLE
   ) PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 
 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
```

#### DEPARTMENT

```sql
CREATE TABLE "SYSTEM"."DEPARTMENT" 
   (	"DEP_ID" VARCHAR2(20) NOT NULL ENABLE, 
	"NAME" VARCHAR2(100) NOT NULL ENABLE, 
	 CONSTRAINT "DEP_PK" PRIMARY KEY ("DEP_ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM"  ENABLE
   ) PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 
 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
```

#### FINANCIAL

```sql
CREATE TABLE "SYSTEM"."FINANCIAL" 
   (	"HOS_ID" VARCHAR2(20), 
	"FINANCE_MONTH" DATE, 
	"INCOME" NUMBER(38,2), 
	"PAYOUT" NUMBER(38,2), 
	 CONSTRAINT "PK_FINANCIAL" PRIMARY KEY ("HOS_ID", "FINANCE_MONTH")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM"  ENABLE, 
	 CONSTRAINT "FINANCIAL_HOS" FOREIGN KEY ("HOS_ID")
	  REFERENCES "SYSTEM"."HOSPITAL" ("HOS_ID") ENABLE
   ) PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 
 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
```

#### GOODS

```sql
CREATE TABLE "SYSTEM"."GOODS" 
   (	"G_ID" VARCHAR2(20) NOT NULL ENABLE, 
	"NAME" VARCHAR2(100) NOT NULL ENABLE, 
	"PRICE" NUMBER(38,2) NOT NULL ENABLE, 
	"PRODUCER" VARCHAR2(20) NOT NULL ENABLE, 
	 CONSTRAINT "GOODS_PK" PRIMARY KEY ("G_ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM"  ENABLE
   ) PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 
 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
```

#### HOSPITAL

```sql
CREATE TABLE "SYSTEM"."HOSPITAL" 
   (	"HOS_ID" VARCHAR2(20) NOT NULL ENABLE, 
	"DATE_OF_ESTABLISHMENT" DATE NOT NULL ENABLE, 
	"NAME" VARCHAR2(100) NOT NULL ENABLE, 
	"QUALIFICATION" VARCHAR2(100) NOT NULL ENABLE, 
	"ADDRESS" VARCHAR2(200) NOT NULL ENABLE, 
	"PHONE_NUMBER" VARCHAR2(20) NOT NULL ENABLE, 
	 CONSTRAINT "HOSPITAL_PK" PRIMARY KEY ("HOS_ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM"  ENABLE
   ) PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 
 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
```

#### HOS_DEP

```sql
CREATE TABLE "SYSTEM"."HOS_DEP" 
   (	"H_ID" VARCHAR2(20) NOT NULL ENABLE, 
	"D_ID" VARCHAR2(20) NOT NULL ENABLE, 
	 CONSTRAINT "HOS_DEP_H_ID_FK_R" FOREIGN KEY ("H_ID")
	  REFERENCES "SYSTEM"."HOSPITAL" ("HOS_ID") ENABLE, 
	 CONSTRAINT "HOS_DEP_D_ID_FK_R" FOREIGN KEY ("D_ID")
	  REFERENCES "SYSTEM"."DEPARTMENT" ("DEP_ID") ENABLE
   ) PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 
 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
```

#### INVENTORY

```sql
CREATE TABLE "SYSTEM"."INVENTORY" 
   (	"STORAGE" NUMBER NOT NULL ENABLE, 
	"HOS_ID" VARCHAR2(20) NOT NULL ENABLE, 
	"G_ID" VARCHAR2(20) NOT NULL ENABLE, 
	"SELL_BY_DATE" DATE NOT NULL ENABLE, 
	 CONSTRAINT "PK_INVENTORY" PRIMARY KEY ("HOS_ID", "G_ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM"  ENABLE, 
	 CONSTRAINT "INVENTORY_HOS_HOS_ID_FK" FOREIGN KEY ("HOS_ID")
	  REFERENCES "SYSTEM"."HOSPITAL" ("HOS_ID") ENABLE, 
	 CONSTRAINT "INV_GOODS_FK" FOREIGN KEY ("G_ID")
	  REFERENCES "SYSTEM"."GOODS" ("G_ID") ENABLE
   ) PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 
 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
```

#### MEMBER

```sql
CREATE TABLE "SYSTEM"."MEMBER" 
   (	"VIPLEVEL" VARCHAR2(20) NOT NULL ENABLE, 
	"DISCOUNT" NUMBER(*,2) NOT NULL ENABLE, 
	 CONSTRAINT "MEMBER_PK" PRIMARY KEY ("VIPLEVEL")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM"  ENABLE
   ) PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 
 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
```

#### OPERATE

```sql
CREATE TABLE "SYSTEM"."OPERATE" 
   (	"PROJ_ID" VARCHAR2(20) NOT NULL ENABLE, 
	"SER_ID" VARCHAR2(20), 
	"BILL_ID" VARCHAR2(20) NOT NULL ENABLE, 
	"FOUND_DATE" DATE NOT NULL ENABLE, 
	"EXE_STATE" VARCHAR2(20) NOT NULL ENABLE, 
	"OP_TIME_ID" VARCHAR2(20), 
	 CONSTRAINT "OPERATE_PK" PRIMARY KEY ("PROJ_ID", "BILL_ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM"  ENABLE, 
	 CONSTRAINT "OPERATE_PROJECT_PROJ_ID_FK_R" FOREIGN KEY ("PROJ_ID")
	  REFERENCES "SYSTEM"."PROJECT" ("PROJ_ID") ENABLE, 
	 CONSTRAINT "OEP_SER_FK" FOREIGN KEY ("SER_ID")
	  REFERENCES "SYSTEM"."SERVER" ("SER_ID") ENABLE, 
	 CONSTRAINT "BIL_PROJ_FK" FOREIGN KEY ("BILL_ID")
	  REFERENCES "SYSTEM"."BILL" ("BILL_ID") ENABLE, 
	 CONSTRAINT "OP_OPTIME_FK" FOREIGN KEY ("OP_TIME_ID")
	  REFERENCES "SYSTEM"."OPERATE_TIME" ("OP_TIME_ID") ENABLE
   ) PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 
 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
```

#### OPERATE_TIME

```sql
CREATE TABLE "SYSTEM"."OPERATE_TIME" 
   (	"OP_TIME_ID" VARCHAR2(20) NOT NULL ENABLE, 
	"DAY" DATE NOT NULL ENABLE, 
	"START_TIME" DATE NOT NULL ENABLE, 
	"END_TIME" DATE NOT NULL ENABLE, 
	"STATUS" VARCHAR2(20) NOT NULL ENABLE, 
	"ROOM_ID" VARCHAR2(20) NOT NULL ENABLE, 
	 CONSTRAINT "OPERATE_TIME_PK" PRIMARY KEY ("OP_TIME_ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM"  ENABLE, 
	 CONSTRAINT "OPERATING_TIME_OPERATING_ROOM_ROOM_ID" FOREIGN KEY ("ROOM_ID")
	  REFERENCES "SYSTEM"."OPERATING_ROOM" ("ROOM_ID") ENABLE
   ) PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 
 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
```

#### OPERATING_ROOM

```sql
CREATE TABLE "SYSTEM"."OPERATING_ROOM" 
   (	"ROOM_ID" VARCHAR2(20) NOT NULL ENABLE, 
	"HOS_ID" VARCHAR2(20) NOT NULL ENABLE, 
	 CONSTRAINT "OPERATING_ROOM_PK" PRIMARY KEY ("ROOM_ID", "HOS_ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM"  ENABLE, 
	 CONSTRAINT "ROOM_UNIQUE" UNIQUE ("ROOM_ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM"  ENABLE, 
	 CONSTRAINT "OPERATING_ROOM_HOS_HOS_ID_FK" FOREIGN KEY ("HOS_ID")
	  REFERENCES "SYSTEM"."HOSPITAL" ("HOS_ID") ENABLE
   ) PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 
 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
```

#### PROJECT

```sql
CREATE TABLE "SYSTEM"."PROJECT" 
   (	"PROJ_ID" VARCHAR2(20) NOT NULL ENABLE, 
	"NAME" VARCHAR2(100) NOT NULL ENABLE, 
	"PRICE" NUMBER(*,2) NOT NULL ENABLE, 
	"FOUND_DATE" DATE NOT NULL ENABLE, 
	 CHECK (PRICE >= 0) ENABLE, 
	 CONSTRAINT "PROJECT_PK" PRIMARY KEY ("PROJ_ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM"  ENABLE
   ) PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 
 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
```

#### PROJ_GOOD

```sql
CREATE TABLE "SYSTEM"."PROJ_GOOD" 
   (	"PROJ_ID" VARCHAR2(20) NOT NULL ENABLE, 
	"G_ID" VARCHAR2(20) NOT NULL ENABLE, 
	 PRIMARY KEY ("PROJ_ID", "G_ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM"  ENABLE, 
	 FOREIGN KEY ("PROJ_ID")
	  REFERENCES "SYSTEM"."PROJECT" ("PROJ_ID") ENABLE, 
	 FOREIGN KEY ("G_ID")
	  REFERENCES "SYSTEM"."GOODS" ("G_ID") ENABLE
   ) PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 
 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
```

#### SERVER

```sql
CREATE TABLE "SYSTEM"."SERVER" 
   (	"SER_ID" VARCHAR2(20) NOT NULL ENABLE, 
	"JOINED_DATE" DATE NOT NULL ENABLE, 
	"NAME" VARCHAR2(100) NOT NULL ENABLE, 
	"PASSWORD" VARCHAR2(20) NOT NULL ENABLE, 
	"POSITION" VARCHAR2(100) NOT NULL ENABLE, 
	"PHONE_NUM" VARCHAR2(20) NOT NULL ENABLE, 
	"HOS_ID" VARCHAR2(20) NOT NULL ENABLE, 
	"DEP_ID" VARCHAR2(20) NOT NULL ENABLE, 
	"WORK_TIME_ID" VARCHAR2(20) NOT NULL ENABLE, 
	"IS_WORK_TODAY" NUMBER(*,0) NOT NULL ENABLE, 
	"GENDER" VARCHAR2(10) NOT NULL ENABLE, 
	"BIRTHDAY" DATE NOT NULL ENABLE, 
	"BASICSALARY" NUMBER(38,2) NOT NULL ENABLE, 
	"TAKEHOMEPAY" NUMBER(38,2) NOT NULL ENABLE, 
	 CONSTRAINT "SERVER_PK" PRIMARY KEY ("SER_ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM"  ENABLE, 
	 CONSTRAINT "SER_HOS_HOS_ID_FK" FOREIGN KEY ("HOS_ID")
	  REFERENCES "SYSTEM"."HOSPITAL" ("HOS_ID") ENABLE, 
	 CONSTRAINT "SER_DEP_DEP_ID_FK" FOREIGN KEY ("DEP_ID")
	  REFERENCES "SYSTEM"."DEPARTMENT" ("DEP_ID") ENABLE, 
	 CONSTRAINT "SER_WORKTIME_FK" FOREIGN KEY ("WORK_TIME_ID")
	  REFERENCES "SYSTEM"."WORK_TIME" ("WORK_TIME_ID") ENABLE
   ) PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 
 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
```

#### WORK_TIME

```sql
CREATE TABLE "SYSTEM"."WORK_TIME" 
   (	"WORK_TIME_ID" VARCHAR2(20) NOT NULL ENABLE, 
	"DAY1" NUMBER(10,0) NOT NULL ENABLE, 
	"DAY2" NUMBER(10,0) NOT NULL ENABLE, 
	"DAY3" NUMBER(10,0) NOT NULL ENABLE, 
	"DAY4" NUMBER(10,0) NOT NULL ENABLE, 
	"DAY5" NUMBER(10,0) NOT NULL ENABLE, 
	"DAY6" NUMBER(10,0) NOT NULL ENABLE, 
	"DAY7" NUMBER(10,0) NOT NULL ENABLE, 
	 PRIMARY KEY ("WORK_TIME_ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM"  ENABLE
   ) PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 
 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "SYSTEM" ;
```