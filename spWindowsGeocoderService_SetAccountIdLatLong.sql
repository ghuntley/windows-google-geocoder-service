USE [Interactive]
GO
/****** Object:  StoredProcedure [dbo].[spWindowsGeocoderService_SetAccountIdLatLong]    Script Date: 2/07/2013 3:13:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  	Geoffrey Huntley
-- Create date: 11th June 2013
-- Description:	Used by the Windows Geocoder Service to update the lat/long of a AccountId.
--				https://github.interactive.com.au/it-apps/windows-google-geocoder-service
-- =============================================

/*
****************** LIST OF CHANGES ******************

Date		Heat#  Initials		Peer	Description	
----------------------------------------------------------------------

										
*/
ALTER PROCEDURE [dbo].[spWindowsGeocoderService_SetAccountIdLatLong]
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

