using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ExampleStruct: IEquatable<ExampleStruct>
{
    public string itemId;
    public string itemName;
    public int num1;
    public long seed;

    public ExampleStruct(string itemId = "")
    {
        this.itemId = itemId;
        this.itemName = string.Empty;
        this.num1 = 0;
        this.seed = -1;
    }

    public ExampleStruct(Func<long> getSeed, string itemId = "")
    {
        DateTimeOffset dateTimeOffset = new DateTimeOffset();
        long createdTime = dateTimeOffset.ToUnixTimeMilliseconds();
        this.itemId = itemId;
        this.itemName = string.Empty;
        this.num1 = 0;
        this.seed = getSeed() * createdTime;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        return obj is ExampleStruct && Equals((ExampleStruct)obj);
    }

    public bool Equals(ExampleStruct other)
    {
        return seed == other.seed;
    }

    public override int GetHashCode()
    {
        return this.seed.GetHashCode();
    }
}
