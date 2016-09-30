using PullToRefresh.Helpers;
using PullToRefresh.Adapters;
using System.Threading.Tasks;
using System;

namespace PullToRefresh.Activities
{
    public class LoadMoreItems : IOnLoadMoreListener
    {
        PhotoAlbum photoAlbum = new PhotoAlbum();
        PhotoAlbumAdapter albumAdapter;
        private int photosCount =10;
        private int PhotosCountTOdisplay = 10;
       
        public Func<bool> LoadingComplete
        {
            get;
            set;
        }

        public LoadMoreItems(PhotoAlbumAdapter adapter)
        {
            albumAdapter = adapter;
        }

        public async Task onLoadMore()
        {
            var task = Task.Run(async () => { await Task.Delay(5000); });
            await task.ContinueWith(GetMore);
            photosCount += photosCount;
        }

        public async Task GetMore(Task task)
        {
            await Task.Run(() =>
            {
                var data = photoAlbum.MyPhotoAlbums.GetRange(photosCount, PhotosCountTOdisplay);
                    albumAdapter.photoAlbum.AddNewPhotos(data);
                LoadingComplete();
            });
        }

        public bool HasMoreItems()
        {
            return photoAlbum.MyPhotoAlbums.Count > photosCount;
        }
    }
}

