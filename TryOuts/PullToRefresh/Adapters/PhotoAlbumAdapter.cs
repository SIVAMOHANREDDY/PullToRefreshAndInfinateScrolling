using System;
using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Support.V7.Widget;
using PullToRefresh.Helpers;
using PullToRefresh.Holders;

namespace PullToRefresh.Adapters
{
    public class PhotoAlbumAdapter : RecyclerView.Adapter
    {
        public event EventHandler<int> ItemClick;
        private  int VIEW_TYPE_ITEM = 0;
        private  int VIEW_TYPE_LOADING = 1;
        public PhotoAlbum photoAlbum;
        public Activity activity;

        public override int ItemCount
        {
            get
            {
                return photoAlbum.NumPhotos;
            }
        }

        public override int GetItemViewType(int position)
        {
            return photoAlbum[position] == null ? VIEW_TYPE_LOADING : VIEW_TYPE_ITEM;
        }

        public PhotoAlbumAdapter(Activity _activity, PhotoAlbum _photoAlbum)
        {
            activity = _activity;
            photoAlbum = _photoAlbum;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (holder is PhotoViewHolder)
            {
                PhotoViewHolder vh = holder as PhotoViewHolder;
                vh.Image.SetImageResource(photoAlbum[position].PhotoID);
                vh.Caption.Text = photoAlbum[position].Caption;
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            if (viewType == VIEW_TYPE_ITEM)
            {
                View itemView = LayoutInflater.From(parent.Context).
                Inflate(Resource.Layout.PhotoCardView, parent, false);
                PhotoViewHolder vh = new PhotoViewHolder(itemView, OnClick);
                return vh;
            }
            return null;
        }
    
        public void AddPhotos(ICollection<Photo> newPhotos)
        {
            photoAlbum.AddNewPhotos(newPhotos);
        }

        public void AddPhotos(int index, ICollection<Photo> newPhotos)
        {
            photoAlbum.AddNewPhotos(index, newPhotos);
        }

        public int getItemCount()
        {
            return photoAlbum == null ? VIEW_TYPE_ITEM : ItemCount;
        }

        void OnClick(int position)
        {
            ItemClick?.Invoke(this, position);
        }
    }
}