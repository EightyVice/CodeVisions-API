public class Link{   

    int data;
    Link next;

    public Link(int a, Link b) {
        this.data = a;
        this.next = b;
    }

    public static void Main()
    {
        Link p = new Link(1, new Link(2, new Link(3, new Link(4, null))));
    }
}
	/*


        public Link iterateOverList(Link p, Link q){
        while(q.next != null){
            q = q.next
        }
        return q;
    }


		Link a = new Link();
		Link y = new Link();
		Link b = a;
		Link c = null;
		int[] arr = {1,2,3,4};
		b.x = 4;
		func1(1);


                // Find sum of array element
        int sum = 0;
         
        for (int i = 0; i < n; i++)
            sum += a[i];
     
        return (double)sum / n;


		----


	// Function that return average of an array.
   static double average(int[] a, int n)
   {
        // Find sum of array element
        int sum = 0;
         
        for (int i = 0; i < n; i++)
            sum += a[i];

        print(sum/n);
   }
     
    //driver code
    public static void Main()
    {
         
        int[] arr = {10, 2, 3, 4, 5, 6, 7, 8, 9};
        int n = arr.length;
        average(arr,n);
    }

	*/