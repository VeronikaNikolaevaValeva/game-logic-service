namespace GameLogicService.Models.Enum
{
    /// <summary>
    /// Different categories of reasons for service call rejection
    /// </summary>
    public enum RejectionCode
    {
        General = 00,
        InsufficientPermission = 10,
        DataValidation = 20,
        NotFound = 30
    }
}
