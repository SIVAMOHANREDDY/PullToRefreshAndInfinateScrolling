using Android.App;
using Android.Widget;
using Android.Support.V7.Widget;
using PullToRefresh.Adapters;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Graphics;
using Android.Views;

namespace PullToRefresh.Activities
{
    public class LoadMoreScrollListener : RecyclerView.OnScrollListener
    {
        private LinearLayoutManager linearLayoutManager;
        private RecyclerView recyclerView;
        private ProgressBar progressBar;
        private ProgressBar progressBarOnSnackBar;
        private PhotoAlbumAdapter adapter;
        IOnLoadMoreListener loadMoreListener;
        private SwipeRefreshLayout swipeRefreshLayout;
        private PullToRefreshActivity pulltoRefreshActivity;
        private Snackbar snackbar;
        private string loadingMessage = "Loading...";
        private string noMoreItemsMessage = "No more Items Found";
        public PhotoAlbumAdapter Adapter
        {
            get
            {
                if (adapter == null)
                    adapter = (recyclerView.GetAdapter() as PhotoAlbumAdapter);
                return adapter;
            }
        }

        public LoadMoreScrollListener(PullToRefreshActivity pulltoRefreshActivity, RecyclerView.LayoutManager manager, ProgressBar progressBar, SwipeRefreshLayout swipeRefreshLayout, IOnLoadMoreListener loadMoreListener)
        {
            linearLayoutManager = (LinearLayoutManager)manager;
            this.progressBar = progressBar;
            this.loadMoreListener = loadMoreListener;
            this.swipeRefreshLayout = swipeRefreshLayout;
            this.pulltoRefreshActivity = pulltoRefreshActivity;
        }

        public async override void OnScrolled(RecyclerView recyclerView, int dx, int dy)
        {
            base.OnScrolled(recyclerView, dx, dy);
            this.recyclerView = recyclerView;
            var visibleItemCount = linearLayoutManager.ChildCount;
            var totalItemCount = linearLayoutManager.ItemCount;
            var pastVisiblesItems = linearLayoutManager.FindFirstVisibleItemPosition();

            if ((visibleItemCount + pastVisiblesItems) >= totalItemCount)
            {
                if (!loadMoreListener.HasMoreItems() || progressBar.Visibility == Android.Views.ViewStates.Visible)
                    return;
                progressBar.Visibility = ViewStates.Visible;
                snackbar = OnSnackbarVisibility(swipeRefreshLayout, loadingMessage, Snackbar.LengthIndefinite);
                snackbar.Show();
                loadMoreListener.LoadingComplete = LoadingCompleted;
                await loadMoreListener.onLoadMore();
            }

            if ((visibleItemCount + pastVisiblesItems) == 20)
            {
                snackbar = OnSnackbarVisibility(swipeRefreshLayout, noMoreItemsMessage, Snackbar.LengthIndefinite);
                snackbar.SetAction("Dismiss", action => { });
                snackbar.Show();
                progressBarOnSnackBar.Visibility = ViewStates.Invisible;
            }
        }

        private Snackbar OnSnackbarVisibility(View view, string text, int duration)
        {
            progressBarOnSnackBar = new ProgressBar(pulltoRefreshActivity.BaseContext, null, Android.Resource.Attribute.ProgressBarStyleSmallTitle);
            progressBarOnSnackBar.SetPadding(0, 40, 0, 40);
            progressBarOnSnackBar.IndeterminateDrawable.SetColorFilter(Color.White, PorterDuff.Mode.SrcIn);
            Snackbar snackBar = Snackbar.Make(view, text, duration);
            Snackbar.SnackbarLayout snack_view = (Snackbar.SnackbarLayout)snackBar.View;
            snack_view.AddView(progressBarOnSnackBar);
            return snackBar;
        }

        private bool LoadingCompleted()
        {
            // Reset your busy indicator
            // Note: UI thread may be needed.
            (Adapter.activity as Activity).RunOnUiThread(() =>
            {
                if (progressBar != null)
                {
                    Adapter.NotifyDataSetChanged();
                }

                snackbar.Dismiss();
                progressBar.Visibility = ViewStates.Invisible;

            });
            return true;
        }
    }
}