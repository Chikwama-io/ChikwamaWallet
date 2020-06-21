using System;
using System.Collections.Generic;
using System.Text;

namespace ChikwamaWallet.Services
{
    public interface IChikwamaLocationService
    {
        //	Define	our	Location	Service	Instance	Methods	
        void GetMyLocation();

        double GetDistanceTravelled(double lat, double lon);
        event EventHandler<IChikwamaLocationCoords> MyLocation;
    }

    //	Walk Location Coordinates	Obtained	
    public interface IChikwamaLocationCoords
    {
        double latitude { get; set; }
        double longitude { get; set; }
    }
}
