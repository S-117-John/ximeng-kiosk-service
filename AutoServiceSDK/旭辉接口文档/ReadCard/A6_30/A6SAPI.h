/*++

Module Name:

    A6SAPI.H

Abstract:

    Master include file for applications that call 
    functions provided by A6SAPI.DLL

Revision History:

    2013-12-02 : created

--*/

#ifndef _A6SAPI_H
#define _A6SAPI_H

#include <Windows.h>

// Return codes of the APIs
//
#define A6_S_SUCCESS			0
#define A6_E_DEV_NOT_RECOGNIZED		0x300
#define A6_E_PORT_UNAVAILABLE		0x303
#define A6_E_UNKNOWN_ERROR		0x311
#define A6_E_COMM_TIMEOUT		0x320
#define A6_E_COMM_ERROR			0x321
#define A6_E_INVALID_HANDLE		0x330
#define A6_E_INVALID_PARAMETER		0x331
#define A6_E_NO_MEMORY			0x332
#define A6_E_BUFFER_TOO_SMALL		0x334
#define A6_E_CANCELLED			0x390
#define A6_E_UNDEFINED_COMMAND		0x400
#define A6_E_COMMAND_DATA_ERROR		0x402
#define A6_E_DISABLED_COMMAND		0x403
#define A6_E_COMMAND_FAILED		0x404
#define A6_E_VOLTAGE_TOO_HIGH		0x405
#define A6_E_VOLTAGE_TOO_LOW		0x406
#define A6_E_POWER_DOWN			0x407
#define A6_E_SENSOR_ABNORMALITY		0x408
#define A6_E_CARD_JAMMED		0x40A
#define A6_E_SHUTTER_ABNORMALITY	0x40B
#define A6_E_TOO_LONG_CARD		0x40C
#define A6_E_TOO_SHORT_CARD		0x40D
#define A6_E_CARD_ABNORMALITY		0x430
#define A6_E_NO_RF_CARD			0x440
#define A6_E_NO_IC_CARD			0x450
#define A6_E_CARD_SCRAPPED		0x460
#define A6_E_INVALID_PASSWORD		0x470
#define A6_E_VALUE_FORMAT_ERROR		0x472


// Data type
//
typedef ULONG_PTR READERHANDLE;
typedef READERHANDLE *PREADERHANDLE;


// APIs
//
LONG	
	WINAPI 
	A6_Connect
	(
	__in	DWORD	dwPort,
	__in	DWORD	dwBaudRate,
	__out	PREADERHANDLE phReader
	);
LONG	
	WINAPI 
	A6_Disconnect
	(
	__in	READERHANDLE hReader
	);
HANDLE	
	WINAPI 
	A6_GetCommHandle
	(
	__in	READERHANDLE hReader
	);
LONG	
	WINAPI
	A6_Cancel
	(
	__in	READERHANDLE hReader
	);
LONG	
	WINAPI 
	A6_ExecCommand
	(
	__in	READERHANDLE hReader,
	__in	BYTE	bCommandCode,
	__in	BYTE	bParameterCode,
	__in	PBYTE	pbBodyData,
	__in	USHORT	cbBodyLength,
	__out	PBYTE	pbReplyData,
	__inout	PDWORD	pcbReplyLength
	);

#define INIT_ONLY		0x30
#define INIT_RETURN_TO_FRONT	0x31
#define INIT_EJECT_TO_REAR	0x32
#define INIT_RETAIN_INSIDE	0x33
LONG 
	WINAPI 
	A6_Initialize
	(
	__in	READERHANDLE hReader,
	__in	BYTE	bMode,
	__in	PSTR	pszVerBuff,
	__inout	PDWORD	pcbVerLength
	);

#define CRS_CARD_AT_FRONT_NH	0x30
#define CRS_CARD_AT_FRONT	0x31
#define CRS_CARD_AT_RF_POS	0x32
#define CRS_CARD_AT_IC_POS	0x33
#define CRS_CARD_AT_REAR	0x34
#define CRS_NO_CARD_IN		0x35
#define CRS_NOT_IN_STDPOS	0x36
LONG 
	WINAPI 
	A6_GetCRStatus
	(
	__in	READERHANDLE hReader,
	__out	PBYTE	pbStatus
	);

#define NUM_SENSORS	7
LONG 
	WINAPI 
	A6_GetSenserSenseInfo
	(
	__in	READERHANDLE hReader,
	__out	BYTE	(&bSenseInfo)[NUM_SENSORS]
	);
LONG
	WINAPI
	A6_GetSenserVoltage
	(
	__in	READERHANDLE hReader,
	__out	float	(&fVoltage)[NUM_SENSORS]
);
LONG 
	WINAPI 
	A6_TakeinFromFront
	(
	__in	READERHANDLE hReader,
	__in	BOOL	fMagOnly
	);
LONG 
	WINAPI 
	A6_TakeinFromRear
	(
	__in	READERHANDLE hReader
	);
LONG 
	WINAPI 
	A6_PermitInsertion
	(
	__in	READERHANDLE hReader,
	__in	BOOL	fMagOnly
	);
LONG 
	WINAPI 
	A6_DenieInsertion
	(
	__in	READERHANDLE hReader
	);

#define MOVE_TO_INTERNAL	0x30
#define MOVE_TO_IC_POS		0x31
#define MOVE_TO_FRONT		0x32
#define MOVE_TO_REAR		0x33
#define MOVE_TO_FRONT_NH	0x34
#define MOVE_TO_REAR_NH		0x35
#define MOVE_TO_REREADING_POS	0x36
LONG 
	WINAPI 
	A6_MoveCard
	(
	__in	READERHANDLE hReader,
	__in	BYTE	bMode
	);
LONG 
	WINAPI
	A6_SetBaudRate(
	__in	READERHANDLE hReader,
	__in	DWORD	dwBaudRate
	);

#define LEDCNTL_OFF	0x30
#define LEDCNTL_ON	0x31
#define LEDCNTL_BLINK	0x32
LONG 
	WINAPI 
	A6_LedControl
	(
	__in	READERHANDLE hReader,
	__in	BYTE	bControlCode
	);

// Mag.card operations
//


#define TRACKST_NORMAL		0x60
#define TRACKST_SS_ERROR	0x61
#define TRACKST_ES_ERROR	0x62
#define TRACKST_PARITY_ERROR	0x63
#define TRACKST_LRC_ERROR	0x64
#define TRACKST_NO_DATA		0x65

typedef struct _TRACKINFO
{
	char	Contents[3][128];
	int	Lengths[3];
	BYTE	Status[3];
} TRACKINFO, *PTRACKINFO;

#define TRACKID_ISO1		0x10
#define TRACKID_ISO2		0x20
#define TRACKID_ISO3		0x40
#define TRACKID_ALL		0x70

#define READ_ASCII		0x37
#define READ_BINARY		0x38

LONG 
	WINAPI 
	A6_ReadTracks
	(
	__in	READERHANDLE hReader,
	__in	BYTE	bMode,
	__in	UINT	iTrackID,
	__out	PTRACKINFO pTrackInfo
	);

// IC Card Operations
//

#define ICCTYPE_AT24C01		0x30
#define ICCTYPE_AT24C02		0x31
#define ICCTYPE_AT24C04		0x32
#define ICCTYPE_AT24C08		0x33
#define ICCTYPE_AT24C16		0x34
#define ICCTYPE_AT24C32		0x35
#define ICCTYPE_AT24C64		0x36
#define ICCTYPE_AT45DB041	0x37
#define ICCTYPE_AT88SC102	0x38
#define ICCTYPE_AT88SC1604	0x39
#define ICCTYPE_AT88SC1608	0x3A
#define ICCTYPE_SLE4442		0x3B
#define ICCTYPE_SLE4428		0x3C
#define ICCTYPE_T0_CPU		0x3D
#define ICCTYPE_T1_CPU		0x3E
#define ICCTYPE_UNKNOWN		0xFF

LONG 
	WINAPI 
	A6_IccPowerOn
	(
	__in	READERHANDLE hReader
	);
LONG 
	WINAPI 
	A6_IccPowerOff
	(
	__in	READERHANDLE hReader
	);
LONG
	WINAPI 
	A6_DetectIccType
	(
	__in	READERHANDLE hReader,
	__out	PBYTE	pbCardType
	);

#define RFCTYPE_MIFARE_UL	0x30
#define RFCTYPE_MIFARE_S50	0x31
#define RFCTYPE_MIFARE_S70	0x32
#define RFCTYPE_TYPEA_CPU	0x33
#define RFCTYPE_TYPEB_CPU	0x34
LONG
	WINAPI
	A6_DetectRfcType
	(
	__in	READERHANDLE hReader,
	__out	PBYTE	pbCardType
	);

#define ICC_PROTOCOL_T0	0x30
#define ICC_PROTOCOL_T1	0x31

#define VOLTAGE_1_8	0x30
#define VOLTAGE_3	0x31
#define VOLTAGE_5	0x32

// CPU Operations
//

LONG
	WINAPI 
	A6_CpuActivate
	(
	__in	READERHANDLE hReader,
	__in	BYTE	bVoltage,
	__out	PBYTE	pbProtocol,
	__out	PBYTE	pbATRBuff,
	__inout	PDWORD	pcbATRLength
	);
LONG 
	WINAPI 
	A6_CpuDeactivate
	(
	__in	READERHANDLE hReader
	);
LONG 
	WINAPI 
	A6_CpuTransmit
	(
	__in	READERHANDLE hReader,
	__in	BYTE	bProtocol,
	__in	PBYTE	pbSendBuff,
	__in	USHORT	cbSendLength,
	__out	PBYTE	pbRecvBuff,
	__inout	PDWORD	pcbRecvLength
	);

// SAM Operations
//

LONG 
	WINAPI
	A6_SamActivate
	(
	__in	READERHANDLE hReader,
	__in	BYTE	bVoltage,
	__out	PBYTE	pbProtocol,
	__out	PBYTE	pbATRBuff,
	__inout	PDWORD	pcbATRLength
	);
LONG 
	WINAPI 
	A6_SamDeactivate
	(
	__in	READERHANDLE hReader
	);
LONG 
	WINAPI 
	A6_SamTransmit
	(
	__in	READERHANDLE hReader,
	__in	BYTE	bProtocol,
	__in	PBYTE	pbSendBuff,
	__in	USHORT	cbSendLength,
	__out	PBYTE	pbRecvBuff,
	__inout	PDWORD	pcbRecvLength
	);
LONG 
	WINAPI
	A6_SamSelect
	(
	__in	READERHANDLE hReader,
	__in	BYTE	bSAMNumber	// 1, 2, 3 ... 
	);

// I2C Operations
//

#define I2CTYPE_24C01		0x30
#define I2CTYPE_24C02		0x31
#define I2CTYPE_24C04		0x32
#define I2CTYPE_24C08		0x33
#define I2CTYPE_24C16		0x34
#define I2CTYPE_24C32		0x35
#define I2CTYPE_24C64		0x36

LONG 
	WINAPI 
	A6_I2cRead
	(
	__in	READERHANDLE hReader,
	__in	BYTE	bCardType,
	__in	WORD	wStartAddress,
	__in	BYTE	bBytesToRead,
	__out	PBYTE	pbBuffer,
	__inout	PDWORD	pcbLength
	);
LONG 
	WINAPI 
	A6_I2cWrite
	(
	__in	READERHANDLE hReader,
	__in	BYTE	bCardType,
	__in	WORD	wStartAddress,
	__in	BYTE	bBytesToWrite,
	__in	PBYTE	pbBuffer
	);

// AT45DB041 Operations

LONG 
	WINAPI 
	A6_Db041Reset
	(
	__in	READERHANDLE hReader
	);
LONG 
	WINAPI 
	A6_Db041ReadPage
	(
	__in	READERHANDLE hReader,
	__in	WORD	wPageAddress,
	__out	BYTE	(&bPageBuffer)[264]
	);
LONG 
	WINAPI 
	A6_Db041WritePage
	(
	__in	READERHANDLE hReader,
	__in	WORD	wPageAddress,
	__in	BYTE	(&bPageBuffer)[264]
	);


#define PERSONALIZATION_TEST	0x30
#define PERSONALIZATION_LOGOUT	0x31
#define PERSONALIZATION_REAL	0x32


// AT88SC102 Operations

LONG
	WINAPI 
	A6_Sc102Reset
	(
	__in	READERHANDLE hReader
	);
LONG 
	WINAPI 
	A6_Sc102VerifySC
	(
	__in	READERHANDLE hReader,
	__in	BYTE	bSCByte1,
	__in	BYTE	bSCByte2
	);
LONG 
	WINAPI 
	A6_Sc102UpdateSC
	(
	__in	READERHANDLE hReader,
	__in	BYTE	bSCByte1,
	__in	BYTE	bSCByte2
	);
LONG 
	WINAPI 
	A6_Sc102ReadMemory
	(
	__in	READERHANDLE hReader,
	__in	BYTE	bStartAddress,
	__in	BYTE	bBytesToRead,
	__out	PBYTE	pbBuffer,
	__inout	PDWORD	pcbLength
	);
LONG 
	WINAPI 
	A6_Sc102WriteMemory
	(
	__in	READERHANDLE hReader, 
	__in	BYTE	bStartAddress, 
	__in	BYTE	bBytesToWrite,
	__in	PBYTE	pbBuffer
	);
LONG 
	WINAPI 
	A6_Sc102EraseMemory
	(
	__in	READERHANDLE hReader, 
	__in	BYTE	bStartAddress, 
	__in	BYTE	bBytesToErase
	);
LONG 
	WINAPI 
	A6_Sc102EraseAZ1
	(
	__in	READERHANDLE hReader,
	__in	BYTE	(&bKeyBytes)[6]
	);
LONG 
	WINAPI 
	A6_Sc102EraseAZ2
	(
	__in	READERHANDLE hReader,
	__in	BOOL	fEC2Enabled,
	__in	BYTE	(&bKeyBytes)[4]
	);
LONG 
	WINAPI 
	A6_Sc102UpdateEZ1
	(
	__in	READERHANDLE hReader,
	__in	BYTE	(&bKeyBytes)[6]
	);
LONG 
	WINAPI 
	A6_Sc102UpdateEZ2
	(
	__in	READERHANDLE hReader,
	__in	BYTE	(&bKeyBytes)[4]
	);
LONG 
	WINAPI 
	A6_Sc102Personalize
	(
	__in	READERHANDLE hReader,
	__in	BYTE	bMode
	);

// AT88SC1604 Operations

#define PWDTYPE_SC		0x30
#define PWDTYPE_SC1		0x31
#define PWDTYPE_EZ1		0x32
#define PWDTYPE_SC2		0x33
#define PWDTYPE_EZ2		0x34
#define PWDTYPE_SC3		0x35
#define PWDTYPE_EZ3		0x36
#define PWDTYPE_SC4		0x37
#define PWDTYPE_EZ4		0x38

LONG 
	WINAPI 
	A6_Sc1604Reset
	(
	__in	READERHANDLE hReader
	);
LONG 
	WINAPI 
	A6_Sc1604VerifyPassword
	(
	__in	READERHANDLE hReader,
	__in	BYTE	bPwdType,
	__in	BYTE	bPwdByte1,
	__in	BYTE	bPwdByte2
	);
LONG 
	WINAPI
	A6_Sc1604UpdatePassword
	(
	__in	READERHANDLE hReader,
	__in	BYTE	bPwdType,
	__in	BYTE	bPwdByte1,
	__in	BYTE	bPwdByte2
	);
LONG 
	WINAPI 
	A6_Sc1604ReadMemory
	(
	__in	READERHANDLE hReader,
	__in	WORD	wStartAddress,
	__in	BYTE	bBytesToRead,
	__out	PBYTE	pbBuffer,
	__inout	PDWORD	pcbLength
	);
LONG 
	WINAPI 
	A6_Sc1604WriteMemory
	(
	__in	READERHANDLE hReader,
	__in	WORD	wStartAddress,
	__in	BYTE	bBytesToWrite,
	__in	PBYTE	pbBuffer
	);
LONG 
	WINAPI 
	A6_Sc1604EraseMemory
	(
	__in	READERHANDLE hReader,
	__in	WORD	wStartAddress,
	__in	BYTE	bBytesToErase
	);
LONG 
	WINAPI
	A6_Sc1604Personalize
	(
	__in	READERHANDLE hReader,
	__in	BYTE	bMode
	);

// AT88SC1608 Operations

#define ZONEID_USER0		0x30
#define ZONEID_USER1		0x31
#define ZONEID_USER2		0x32
#define ZONEID_USER3		0x33
#define ZONEID_USER4		0x34
#define ZONEID_USER5		0x35
#define ZONEID_USER6		0x36
#define ZONEID_USER7		0x37
#define ZONEID_CONFIG		0x38

#define RPW_USER0		0x30
#define RPW_USER1		0x31
#define RPW_USER2		0x32
#define RPW_USER3		0x33
#define RPW_USER4		0x34
#define RPW_USER5		0x35
#define RPW_USER6		0x36
#define RPW_USER7		0x37
#define WPW_USER0		0x38
#define WPW_USER1		0x39
#define WPW_USER2		0x3A
#define WPW_USER3		0x3B
#define WPW_USER4		0x3C
#define WPW_USER5		0x3D
#define WPW_USER6		0x3E
#define WPW_USER7		0x3F

LONG 
	WINAPI 
	A6_Sc1608Reset
	(
	__in	READERHANDLE hReader
	);
LONG 
	WINAPI 
	A6_Sc1608VerifyPassword
	(
	__in	READERHANDLE hReader,
	__in	BYTE	bPwdType,
	__in	BYTE	(&bPwdBytes)[3]
	);
LONG 
	WINAPI
	A6_Sc1608UpdatePassword
	(
	__in	READERHANDLE hReader,
	__in	BYTE	bPwdType,
	__in	BYTE	(&bPwdBytes)[3]
	);
LONG 
	WINAPI 
	A6_Sc1608ReadZone
	(
	__in	READERHANDLE hReader,
	__in	BYTE	bZoneID,
	__in	BYTE	bAddress,
	__in	BYTE	bBytesToRead,
	__out	PBYTE	pbBuffer,
	__inout	PDWORD	pcbLength
	);
LONG 
	WINAPI 
	A6_Sc1608WriteZone
	(
	__in	READERHANDLE hReader,
	__in	BYTE	bZoneID,
	__in	BYTE	bAddress,
	__in	BYTE	bBytesToWrite,
	__in	PBYTE	pbBuffer
	);

#define FUSE_BURNED		0x30
#define FUSE_UNBURNED		0x31
LONG 
	WINAPI 
	A6_Sc1608ReadFuses
	(
	__in	READERHANDLE hReader,
	__out	PBYTE	pbFAB,
	__out	PBYTE	pbCMA,
	__out	PBYTE	pbPER
	);
LONG 
	WINAPI 
	A6_Sc1608WriteFuses
	(
	__in	READERHANDLE hReader
	);
LONG 
	WINAPI 
	A6_Sc1608InitAuth
	(
	__in	READERHANDLE hReader,
	__in	BYTE	(&bRandomNumberBytes)[8]
	);
LONG 
	WINAPI 
	A6_Sc1608VerifyAuth
	(
	__in	READERHANDLE hReader,
	__in	BYTE	(&bChallengeBytes)[8]
	);


// SLE4428 Operations

LONG 
	WINAPI 
	A6_Sle4428Reset
	(
	__in	READERHANDLE hReader
	);
LONG 
	WINAPI 
	A6_Sle4428VerifyPSC
	(
	__in	READERHANDLE hReader,
	__in	BYTE	bPSCByte1,
	__in	BYTE	bPSCByte2
	);
LONG 
	WINAPI 
	A6_Sle4428UpdatePSC
	(
	__in	READERHANDLE hReader,
	__in	BYTE	bPSCByte1,
	__in	BYTE	bPSCByte2
	);
LONG 
	WINAPI 
	A6_Sle4428ReadWithoutPB
	(
	__in	READERHANDLE hReader,
	__in	WORD	wStartAddress, 
	__in	BYTE	bBytesToRead,
	__out	PBYTE	pbBuffer, 
	__inout	PDWORD	pcbLength
	);
LONG 
	WINAPI 
	A6_Sle4428ReadProtectionBits
	(
	__in	READERHANDLE hReader,
	__in	WORD	wStartAddress, 
	__in	BYTE	bBytesToRead,
	__out	PBYTE	pbBuffer, 
	__inout	PDWORD	pcbLength
	);
LONG 
	WINAPI 
	A6_Sle4428WriteWithPB
	(
	__in	READERHANDLE hReader,
	__in	WORD	wStartAddress, 
	__in	BYTE	bBytesToWrite,
	__in	PBYTE	pbBuffer
	);
LONG 
	WINAPI 
	A6_Sle4428WriteWithoutPB
	(
	__in	READERHANDLE hReader,
	__in	WORD	wStartAddress, 
	__in	BYTE	bBytesToWrite,
	__in	PBYTE	pbBuffer
	);


// SLE4442 Operations

LONG 
	WINAPI 
	A6_Sle4442Reset
	(
	__in	READERHANDLE hReader
	);
LONG 
	WINAPI 
	A6_Sle4442VerifyPSC
	(
	__in	READERHANDLE hReader,
	__in	BYTE	bPSCByte1,
	__in	BYTE	bPSCByte2,
	__in	BYTE	bPSCByte3
	);
LONG 
	WINAPI 
	A6_Sle4442UpdatePSC
	(
	__in	READERHANDLE hReader,
	__in	BYTE	bPSCByte1,
	__in	BYTE	bPSCByte2,
	__in	BYTE	bPSCByte3
	);
LONG 
	WINAPI 
	A6_Sle4442ReadMainMemory
	(
	__in	READERHANDLE hReader,
	__in	BYTE	bStartAddress,
	__in	BYTE	bBytesToRead,
	__out	PBYTE	pbBuffer, 
	__inout	PDWORD	pcbLength
	);
LONG 
	WINAPI 
	A6_Sle4442WriteMainMemory
	(
	__in	READERHANDLE hReader,
	__in	BYTE	bStartAddress, 
	__in	BYTE	bBytesToWrite,
	__in	PBYTE	pbBuffer
	);
LONG 
	WINAPI 
	A6_Sle4442ReadSecurityMemory
	(
	__in	READERHANDLE hReader,
	__out	BYTE	(&bBuffer)[4]
	);
LONG 
	WINAPI 
	A6_Sle4442ReadProtectionBits
	(
	__in	READERHANDLE hReader,
	__in	BYTE	bStartAddress,
	__in	BYTE	bBytesToRead,
	__out	PBYTE	pbBuffer, 
	__inout	PDWORD	pcbLength
	);
LONG 
	WINAPI 
	A6_Sle4442WriteProtectionMemory
	(
	__in	READERHANDLE hReader,
	__in	BYTE	bStartAddress,
	__in	BYTE	bBytesToWrite,
	__in	PBYTE	pbBuffer
	);

// S50 Operations


LONG 
	WINAPI 
	A6_S50Select
	(
	__in	READERHANDLE hReader
	);
LONG 
	WINAPI 
	A6_S50ReadUID
	(
	__in	READERHANDLE hReader,
	__out	PBYTE	pbUIDBuff,
	__inout	PDWORD	pcbUIDLength
	);
LONG 
	WINAPI 
	A6_S50VerifyPassword
	(
	__in	READERHANDLE hReader,
	__in	BYTE	bSectorNumber,
	__in	BOOL	bWithKeyA,
	__in	BYTE	(&bKeyBytes)[6]
	);
LONG 
	WINAPI 
	A6_S50ReadOneBlock
	(
	__in	READERHANDLE hReader,
	__in	BYTE	bSectorNumber,
	__in	BYTE	bBlockNumber,
	__out	BYTE	(&bBlockBuff)[16]
	);
LONG 
	WINAPI 
	A6_S50WriteOneBlock
	(
	__in	READERHANDLE hReader,
	__in	BYTE	bSectorNumber,
	__in	BYTE	bBlockNumber,
	__in	BYTE	(&bBlockBuff)[16]
	);
LONG 
	WINAPI 
	A6_S50InitializeValue
	(
	__in	READERHANDLE hReader,
	__in	BYTE	bSectorNumber,
	__in	BYTE	bBlockNumber,
	__in	UINT32	uValue
	);
LONG 
	WINAPI 
	A6_S50IncrementValue
	(
	__in	READERHANDLE hReader,
	__in	BYTE	bSectorNumber,
	__in	BYTE	bBlockNumber,
	__in	UINT32	uValue
	);
LONG 
	WINAPI 
	A6_S50DecrementValue
	(
	__in	READERHANDLE hReader,
	__in	BYTE	bSectorNumber,
	__in	BYTE	bBlockNumber,
	__in	UINT32	uValue
	);
LONG
	WINAPI
	A6_S50ReadValue
	(
	__in	READERHANDLE hReader,
	__in	BYTE	bSectorNumber,
	__in	BYTE	bBlockNumber,
	__out	PUINT32	puValue
	);

// S70 Operations

LONG 
	WINAPI 
	A6_S70Select
	(
	__in	READERHANDLE hReader
	);
LONG 
	WINAPI 
	A6_S70ReadUID
	(
	__in	READERHANDLE hReader,
	__out	PBYTE	pbUIDBuff,
	__inout	PDWORD	pcbUIDLength
	);
LONG 
	WINAPI 
	A6_S70VerifyPassword
	(
	__in	READERHANDLE hReader,
	__in	BYTE	bSectorNumber,
	__in	BOOL	bWithKeyA,
	__in	BYTE	(&bKeyBytes)[6]
	);
LONG 
	WINAPI 
	A6_S70ReadOneBlock
	(
	__in	READERHANDLE hReader,
	__in	BYTE	bSectorNumber,
	__in	BYTE	bBlockNumber,
	__out	BYTE	(&bBlockBuff)[16]
	);
LONG
	WINAPI 
	A6_S70WriteOneBlock
	(
	__in	READERHANDLE hReader,
	__in	BYTE	bSectorNumber,
	__in	BYTE	bBlockNumber,
	__in	BYTE	(&bBlockBuff)[16]
	);
LONG 
	WINAPI 
	A6_S70InitializeValue
	(
	__in	READERHANDLE hReader,
	__in	BYTE	bSectorNumber,
	__in	BYTE	bBlockNumber,
	__in	UINT32	uValue
	);
LONG 
	WINAPI 
	A6_S70IncrementValue
	(
	__in	READERHANDLE hReader,
	__in	BYTE	bSectorNumber,
	__in	BYTE	bBlockNumber,
	__in	UINT32	uValue
	);
LONG 
	WINAPI 
	A6_S70DecrementValue
	(
	__in	READERHANDLE hReader,
	__in	BYTE	bSectorNumber,
	__in	BYTE	bBlockNumber,
	__in	UINT32	uValue
	);
LONG
	WINAPI
	A6_S70ReadValue
	(
	__in	READERHANDLE hReader,
	__in	BYTE	bSectorNumber,
	__in	BYTE	bBlockNumber,
	__out	PUINT32	puValue
	);
// Ultralight Operations

LONG 
	WINAPI 
	A6_UlSelect
	(
	__in	READERHANDLE hReader
	);
LONG 
	WINAPI 
	A6_UlReadUID
	(
	__in	READERHANDLE hReader,
	__out	PBYTE	pbUIDBuff,
	__inout	PDWORD	pcbUIDLength
	);
LONG 
	WINAPI 
	A6_UlReadSector
	(
	__in	READERHANDLE hReader,
	__in	BYTE	bSectorNumber,
	__out	BYTE	(&bBuffer)[4]
	);
LONG 
	WINAPI 
	A6_UlWriteSector
	(
	__in	READERHANDLE hReader,
	__in	BYTE	bSectorNumber,
	__in	BYTE	(&bBuffer)[4]
	);
LONG 
	WINAPI 
	A6_UlHalt
	(
	__in	READERHANDLE hReader
	);

// TypeA/B CPU Operations

LONG 
	WINAPI 
	A6_TypeABCpuActivate
	(
	__in	READERHANDLE hReader,
	__in	LPBYTE	pbATRBuff,
	__inout	PDWORD	pcbATRLength
	);
LONG 
	WINAPI 
	A6_TypeABCpuTransmit
	(
	__in	READERHANDLE hReader,
	__in	PBYTE	pbSendBuff,
	__in	USHORT	cbSendLength,
	__in	BOOL	fSendMBit,
	__out	PBYTE	pbRecvBuff,
	__inout	PDWORD	pcbRecvLength,
	__out	PBOOL	pfRecvMBit
	);

#endif // _A6SAPI_H