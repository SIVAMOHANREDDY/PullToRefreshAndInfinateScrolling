using System;
using System.Collections.Generic;
using System.Linq;

namespace PullToRefresh.Helpers
{
    // Photo: contains image resource ID and caption:
    public class Photo
    {
        public int mPhotoID;
        public string mCaption;
        public int PhotoID
        {
            get { return mPhotoID; }
        }
        public string Caption
        {
            get { return mCaption; }
        }
    }

    public class PhotoAlbum
    {
        private static List<Photo> _photosToAdd;
        public List<Photo> mPhotos;
        Random mRandom;
        // Return the number of photos in the photo album:
        public int NumPhotos
        {
            get { return mPhotos != null ? mPhotos.Count : 0; }
        }

        public List<Photo> MyPhotoAlbums
        {
            get
            {
                return mBuiltInPhotos.ToList<Photo>();
            }
        }

        public static Photo[] mBuiltInPhotos = {
            new Photo { mPhotoID = Resource.Drawable.louvre_1,
                        mCaption = "The Louvre" },
            new Photo { mPhotoID = Resource.Drawable.before_mobile_phones,
                        mCaption = "Before mobile phones" },
            new Photo { mPhotoID = Resource.Drawable.big_ben_1,
                        mCaption = "Big Ben skyline" },
            new Photo { mPhotoID = Resource.Drawable.big_ben_2,
                        mCaption = "Big Ben from below" },
            new Photo { mPhotoID = Resource.Drawable.london_eye,
                        mCaption = "The London Eye" },
            new Photo { mPhotoID = Resource.Drawable.eurostar,
                        mCaption = "Eurostar Train" },
            new Photo { mPhotoID = Resource.Drawable.arc_de_triomphe,
                        mCaption = "Arc de Triomphe" },
            new Photo { mPhotoID = Resource.Drawable.louvre_2,
                        mCaption = "Inside the Louvre" },
            new Photo { mPhotoID = Resource.Drawable.versailles_fountains,
                        mCaption = "Versailles fountains" },
            new Photo { mPhotoID = Resource.Drawable.modest_accomodations,
                        mCaption = "Modest accomodations" },
            new Photo { mPhotoID = Resource.Drawable.notre_dame,
                        mCaption = "Notre Dame" },
            new Photo { mPhotoID = Resource.Drawable.inside_notre_dame,
                        mCaption = "Inside Notre Dame" },
            new Photo { mPhotoID = Resource.Drawable.seine_river,
                        mCaption = "The Seine" },
            new Photo { mPhotoID = Resource.Drawable.rue_cler,
                        mCaption = "Rue Cler" },
            new Photo { mPhotoID = Resource.Drawable.champ_elysees,
                        mCaption = "The Avenue des Champs-Elysees" },
            new Photo { mPhotoID = Resource.Drawable.seine_barge,
                        mCaption = "Seine barge" },
            new Photo { mPhotoID = Resource.Drawable.versailles_gates,
                        mCaption = "Gates of Versailles" },
            new Photo { mPhotoID = Resource.Drawable.edinburgh_castle_2,
                        mCaption = "Edinburgh Castle" },
            new Photo { mPhotoID = Resource.Drawable.edinburgh_castle_1,
                        mCaption = "Edinburgh Castle up close" },
            new Photo { mPhotoID = Resource.Drawable.old_meets_new,
                        mCaption = "Old meets new" },
            new Photo { mPhotoID = Resource.Drawable.edinburgh_from_on_high,
                        mCaption = "Edinburgh from on high" },
            new Photo { mPhotoID = Resource.Drawable.edinburgh_station,
                        mCaption = "Edinburgh station" },
            new Photo { mPhotoID = Resource.Drawable.scott_monument,
                        mCaption = "Scott Monument" },
            new Photo { mPhotoID = Resource.Drawable.view_from_holyrood_park,
                        mCaption = "View from Holyrood Park" },
            new Photo { mPhotoID = Resource.Drawable.tower_of_london,
                        mCaption = "Outside the Tower of London" },
            new Photo { mPhotoID = Resource.Drawable.tower_visitors,
                        mCaption = "Tower of London visitors" },
            new Photo { mPhotoID = Resource.Drawable.one_o_clock_gun,
                        mCaption = "One O'Clock Gun" },
            new Photo { mPhotoID = Resource.Drawable.victoria_albert,
                        mCaption = "Victoria and Albert Museum" },
            new Photo { mPhotoID = Resource.Drawable.royal_mile,
                        mCaption = "The Royal Mile" },
            new Photo { mPhotoID = Resource.Drawable.museum_and_castle,
                        mCaption = "Edinburgh Museum and Castle" },
            new Photo { mPhotoID = Resource.Drawable.portcullis_gate,
                        mCaption = "Portcullis Gate" },
            new Photo { mPhotoID = Resource.Drawable.to_notre_dame,
                        mCaption = "Left or right?" },
            new Photo { mPhotoID = Resource.Drawable.pompidou_centre,
                        mCaption = "Pompidou Centre" },
            new Photo { mPhotoID = Resource.Drawable.heres_lookin_at_ya,
                        mCaption = "Here's Lookin' at Ya!" },
            };

        // create the random number generator:
        public PhotoAlbum()
        {
            var photos = new List<Photo>();
            photos.AddRange(mBuiltInPhotos.Take(9));
            mPhotos = photos;
            mRandom = new Random();
        }

        public static List<Photo> PhotosToAdd
        {
            get
            {
                if (_photosToAdd == null)
                    _photosToAdd = new List<Photo>
                    {
                        new Photo { mPhotoID = Resource.Drawable.buckingham_guards,
                                mCaption = "Buckingham Palace" },
                        new Photo { mPhotoID = Resource.Drawable.la_tour_eiffel,
                                mCaption = "The Eiffel Tower" }
                    };

                return _photosToAdd;
            }
        }

        // Indexer (read only) for accessing a photo:
        public Photo this[int i]
        {
            get { return mPhotos[i]; }
        }

        // Pick a random photo and swap it with the top:
        public int RandomSwap()
        {
            Photo tmpPhoto = mPhotos[0];
            int rnd = mRandom.Next(1, mPhotos.Count);
            mPhotos[0] = mPhotos[rnd];
            mPhotos[rnd] = tmpPhoto;
            return rnd;
        }

        public void Shuffle()
        {
            for (int idx = 0; idx < mPhotos.Count; ++idx)
            {
                Photo tmpPhoto = mPhotos[idx];
                int rnd = mRandom.Next(idx, mPhotos.Count);

                mPhotos[idx] = mPhotos[rnd];
                mPhotos[rnd] = tmpPhoto;
            }
        }

        // Add new photos
        public void AddNewPhotos(ICollection<Photo> newPhotos)
        {
            if (newPhotos != null)
                mPhotos.AddRange(newPhotos);
            else
            mPhotos.Add(null);
        }

        public void AddNewPhotos(int index, ICollection<Photo> newPhotos)
        {
            mPhotos.InsertRange(index,newPhotos);
        }
    }
}
