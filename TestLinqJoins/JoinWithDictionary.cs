using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace TestLinqJoins
{

	public class EqualityComparer<T> : IEqualityComparer<T>
	{
		#region IEqualityComparer implementation

		public bool Equals (T x, T y)
		{
			return x.Equals (y);
		}

		public int GetHashCode (T obj)
		{
			return obj.GetHashCode ();
		}

		#endregion

	}


	public class Attempt
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}

	public class Condition
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}

	[TestFixture]
    public class JoinWithDictionary
	{
		[Test]
        public void TestJoinWithDictionary ()
		{
			Dictionary<int, Attempt> allAttempts = 
				new Dictionary<int, Attempt>(new EqualityComparer<int>());

			allAttempts.Add (1, new Attempt () {Id=1, Name="attempt for 1"} );
			allAttempts.Add (2, new Attempt () {Id=2, Name="attempt for 2"} );

			List<Condition> conditions = new List<Condition> ();
			conditions.Add (new Condition() {Id=1, Name="Condition 1"});

			// Cast down to IEnumerable
			IEnumerable<KeyValuePair<int, Attempt>> attempts = allAttempts;

			var result = conditions.Join(
				attempts,
	            c => c.Id,
	            cA => cA.Key,
	            (c, cA) => cA.Value,
	            new EqualityComparer<Int32> ());

			Assert.AreEqual(1, result.Count());
		}


		[Test]
		[Ignore ("another time")]
		public void Ignore ()
		{
			Assert.True (false);
		}
	}
}
