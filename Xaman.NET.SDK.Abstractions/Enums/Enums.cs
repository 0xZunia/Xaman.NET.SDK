using System;
using System.ComponentModel.DataAnnotations;

namespace Xaman.NET.SDK.Abstractions.Enums;

/// <summary>
/// KYC status values
/// </summary>
public enum KycStatus
{
    /// <summary>
    /// No KYC status
    /// </summary>
    [Display(Name = "NONE")]
    None,
    
    /// <summary>
    /// KYC is in progress
    /// </summary>
    [Display(Name = "IN_PROGRESS")]
    InProgress,
    
    /// <summary>
    /// KYC was rejected
    /// </summary>
    [Display(Name = "REJECTED")]
    Rejected,
    
    /// <summary>
    /// KYC was successful
    /// </summary>
    [Display(Name = "SUCCESSFUL")]
    Successful
}

/// <summary>
/// XRPL transaction types
/// </summary>
public enum XrplTransactionType
{
    /// <summary>
    /// AccountDelete transaction type
    /// </summary>
    AccountDelete,
    
    /// <summary>
    /// AccountSet transaction type
    /// </summary>
    AccountSet,
    
    /// <summary>
    /// CheckCancel transaction type
    /// </summary>
    CheckCancel,
    
    /// <summary>
    /// CheckCash transaction type
    /// </summary>
    CheckCash,
    
    /// <summary>
    /// CheckCreate transaction type
    /// </summary>
    CheckCreate,
    
    /// <summary>
    /// DepositPreauth transaction type
    /// </summary>
    DepositPreauth,
    
    /// <summary>
    /// EscrowCancel transaction type
    /// </summary>
    EscrowCancel,
    
    /// <summary>
    /// EscrowCreate transaction type
    /// </summary>
    EscrowCreate,
    
    /// <summary>
    /// EscrowFinish transaction type
    /// </summary>
    EscrowFinish,
    
    /// <summary>
    /// NFTokenAcceptOffer transaction type
    /// </summary>
    NFTokenAcceptOffer,
    
    /// <summary>
    /// NFTokenBurn transaction type
    /// </summary>
    NFTokenBurn,
    
    /// <summary>
    /// NFTokenCancelOffer transaction type
    /// </summary>
    NFTokenCancelOffer,
    
    /// <summary>
    /// NFTokenCreateOffer transaction type
    /// </summary>
    NFTokenCreateOffer,
    
    /// <summary>
    /// NFTokenMint transaction type
    /// </summary>
    NFTokenMint,
    
    /// <summary>
    /// OfferCancel transaction type
    /// </summary>
    OfferCancel,
    
    /// <summary>
    /// OfferCreate transaction type
    /// </summary>
    OfferCreate,
    
    /// <summary>
    /// Payment transaction type
    /// </summary>
    Payment,
    
    /// <summary>
    /// PaymentChannelClaim transaction type
    /// </summary>
    PaymentChannelClaim,
    
    /// <summary>
    /// PaymentChannelCreate transaction type
    /// </summary>
    PaymentChannelCreate,
    
    /// <summary>
    /// PaymentChannelFund transaction type
    /// </summary>
    PaymentChannelFund,
    
    /// <summary>
    /// SetRegularKey transaction type
    /// </summary>
    SetRegularKey,
    
    /// <summary>
    /// SignerListSet transaction type
    /// </summary>
    SignerListSet,
    
    /// <summary>
    /// TicketCreate transaction type
    /// </summary>
    TicketCreate,
    
    /// <summary>
    /// TrustSet transaction type
    /// </summary>
    TrustSet
}

/// <summary>
/// Xaman transaction types (pseudo transaction types)
/// </summary>
public enum XamanTransactionType
{
    /// <summary>
    /// SignIn transaction type (pseudo transaction for authentication)
    /// </summary>
    SignIn
}

/// <summary>
/// XRPL payment flags
/// </summary>
[Flags]
public enum XrplPaymentFlags
{
    /// <summary>
    /// Do not use the default path; only use paths included in the Paths field.
    /// </summary>
    tfNoDirectRipple = 65536,
    
    /// <summary>
    /// If the specified Amount cannot be sent without spending more than SendMax, reduce the received amount instead of failing outright.
    /// </summary>
    tfPartialPayment = 131072,
    
    /// <summary>
    /// Only take paths where all the conversions have an input:output ratio that is equal or better than the ratio of Amount:SendMax.
    /// </summary>
    tfLimitQuality = 262144
}

/// <summary>
/// XRPL trust set flags
/// </summary>
[Flags]
public enum XrplTrustSetFlags
{
    /// <summary>
    /// Authorize the other party to hold currency issued by this account.
    /// </summary>
    tfSetfAuth = 65536,
    
    /// <summary>
    /// Enable the No Ripple flag, which blocks rippling between two trust lines of the same currency if this flag is enabled on both.
    /// </summary>
    tfSetNoRipple = 131072,
    
    /// <summary>
    /// Disable the No Ripple flag, allowing rippling on this trust line.
    /// </summary>
    tfClearNoRipple = 262144,
    
    /// <summary>
    /// Freeze the trust line.
    /// </summary>
    tfSetFreeze = 1048576,
    
    /// <summary>
    /// Unfreeze the trust line.
    /// </summary>
    tfClearFreeze = 2097152
}