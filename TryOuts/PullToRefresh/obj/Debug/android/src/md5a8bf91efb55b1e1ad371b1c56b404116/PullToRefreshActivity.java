package md5a8bf91efb55b1e1ad371b1c56b404116;


public class PullToRefreshActivity
	extends android.support.v7.app.AppCompatActivity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("PullToRefresh.Activities.PullToRefreshActivity, PullToRefresh, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", PullToRefreshActivity.class, __md_methods);
	}


	public PullToRefreshActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == PullToRefreshActivity.class)
			mono.android.TypeManager.Activate ("PullToRefresh.Activities.PullToRefreshActivity, PullToRefresh, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
