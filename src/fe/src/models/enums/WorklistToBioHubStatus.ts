export enum WorklistToBioHubStatus {
  RequestInitiation = 0,
  SubmitAnnex2OfSMTA1 = 4,
  WaitingForAnnex2OfSMTA1SECsApproval = 5,
  SubmitBookingFormOfSMTA1 = 6,
  WaitForBookingFormSMTA1OPSApproval = 7,
  SubmitSMTA1ShipmentDocuments = 8,
  WaitForPickUpCompleted = 9,
  WaitForDeliveryCompleted = 10,
  WaitForArrivalConditionCheck = 11,
  WaitForCommentBHFSendFeedback = 12,
  WaitForFinalApproval = 13,
  ShipmentCompleted = 14,
}
