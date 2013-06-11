USE [Interactive]

GO
-- =============================================
-- Author:		Geoffrey Huntley
-- Create date: 11th June 2013
-- Description:	Used by the Windows Geocoder Service to update the lat/long of a AccountId.
--				https://github.interactive.com.au/it-apps/windows-google-geocoder-service
-- =============================================

/*
****************** LIST OF CHANGES ******************

Date		Heat#  Initials		Peer	Description
----------------------------------------------------------------------


*/
CREATE PROCEDURE [dbo].[spWindowsGeocoderService_SetAccountIdLatLong]
(
@AccountID INT,
@Latitude DECIMAL (10,7),
@Longitude DECIMAL (10,7)
)
AS
SET NOCOUNT ON
BEGIN
UPDATE dbo.DEBTORS
SET Latitude = @Latitude,
Longitude = @Longitude
WHERE AccountID = @AccountID;
END

-- Record the returned geocode data whether successful or not
-- INSERT INTO dbo.DEBTORS_GEOCODE_HISTORY
--	(AccountID, Date, [Status], ResultType, LocationType, PartialMatch,
--	 DebtorAddress, GeocodeAddress, Latitude, Longitude)
-- VALUES
--	(@AccountID, GETDATE(), @Status, @ResultType, @LocationType, @PartialMatch,
--	 @DebtorAddress, @GeocodeAddress, @Latitude, @Longitude)

SET NOCOUNT OFF

GO