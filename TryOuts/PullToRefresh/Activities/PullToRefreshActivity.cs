using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.Widget;
using PullToRefresh.Adapters;
using PullToRefresh.Helpers;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Graphics;
using System.Threading.Tasks;

namespace PullToRefresh.Activities
{
    [Activity(Label = "PullToRefresh", MainLauncher = true, Icon = "@drawable/icon")]
    public class PullToRefreshActivity : AppCompatActivity
    {
        private RecyclerView recycler;
        private RecyclerView.LayoutManager layoutMgr;
        private PhotoAlbumAdapter albumAdapter;
        private SwipeRefreshLayout swipeRefreshLayout;
        private PhotoAlbum photoAlbum;
        private ProgressBar progressbar;
        private int sleepTime = 3000;
        public int totalItemCount = 36;
        private int itemPosition = 0;
        private string refreshedText = "Refreshed!!";
        public string selectedItemText = "Selection: #";
        public PhotoAlbum Photoalbum
        {
            get
            {
                if (photoAlbum == null)
                    photoAlbum = new PhotoAlbum();
                return photoAlbum;
            }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.PullToRefreshView);
            recycler = initRecycler();
            swipeRefreshLayout = initSwipeRefreshLayout();
            progressbar = initProgressBar();
            recycler.AddOnScrollListener(new LoadMoreScrollListener(this, layoutMgr, progressbar, swipeRefreshLayout, new LoadMoreItems(albumAdapter)));
        }

        private RecyclerView initRecycler()
        {
            recycler = FindViewById<RecyclerView>(Resource.Id.placesRecycler);
            layoutMgr = new LinearLayoutManager(this);
            photoAlbum = new PhotoAlbum();
            albumAdapter = new PhotoAlbumAdapter(this, photoAlbum);
            albumAdapter.ItemClick += OnItemClick;

            recycler.SetLayoutManager(layoutMgr);
            recycler.SetAdapter(albumAdapter);
            return recycler;
        }

        private ProgressBar initProgressBar()
        {
            progressbar = FindViewById<ProgressBar>(Resource.Id.progressBar1);
            progressbar.IndeterminateDrawable.SetColorFilter(Color.Red, PorterDuff.Mode.SrcIn);
            progressbar.Visibility = Android.Views.ViewStates.Invisible;
            return progressbar;
        }

        private SwipeRefreshLayout initSwipeRefreshLayout()
        {
            swipeRefreshLayout = FindViewById<SwipeRefreshLayout>(Resource.Id.refresher);
            swipeRefreshLayout.Refresh += SwipeRefreshLayout_Refresh;
            swipeRefreshLayout.SetColorSchemeResources(Android.Resource.Color.HoloBlueLight,
                                                      Android.Resource.Color.HoloGreenLight,
                                                      Android.Resource.Color.HoloOrangeLight,
                                                      Android.Resource.Color.HoloRedLight);
            return swipeRefreshLayout;
        }

        private async void SwipeRefreshLayout_Refresh(object sender, System.EventArgs e)
        {
            await Task.Delay(sleepTime);

            if (albumAdapter.ItemCount < totalItemCount)
            {
                var newPhotos = PhotoAlbum.PhotosToAdd;
                albumAdapter.AddPhotos(itemPosition, newPhotos);
            }

            if (albumAdapter.ItemCount <= totalItemCount)
                albumAdapter.NotifyDataSetChanged();

            (sender as SwipeRefreshLayout).Refreshing = false;
            recycler.SmoothScrollToPosition(itemPosition);
            Toast.MakeText(this, refreshedText, ToastLength.Long).Show();
        }

        void OnItemClick(object sender, int e)
        {
            Toast.MakeText(this, selectedItemText + (e + 1), ToastLength.Short).Show();
        }
    }
}


