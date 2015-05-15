module TSLinq
{
    export class Linq<T>
    {
        constructor(private a: T[] = [])
        {
            Object.defineProperty(this, "a", { value: a, writable: false });
        }

        Aggregate<TResult>(func: (previous: T, next: T) => TResult, initialValue?: T): T
        {
            var a;
            var current;

            if ((a = this.a).length === 0)
                throw "Aggregate of empty array";

            current = a[0];

            for (var i = 1, n = a.length; i < n; ++i)
            {
                current = func(current, a[i]);
            }

            return current;
        }

        All(predicate: (value: T) => boolean): boolean
        {
            var a = this.a;
            for (var i = 0, n = a.length; i < n; ++i)
            {
                if (!predicate(a[i]))
                {
                    return false;
                }
            }

            return true;
        }

        Any(predicate: (value: T) => boolean): boolean
        {
            var a = this.a;
            for (var i = 0, n = a.length; i < n; ++i)
            {
                if (predicate(a[i]))
                {
                    return true;
                }
            }

            return false;
        }

        Average(selector?: (value: T) => number): number
        {
            var a = this.a;

            selector = selector || (o => <any>o);

            var total = 0;

            var len = a.length;
            for (var i = 0; i < len; ++i)
            {
                total += selector(a[i]);
            }

            return total / len;
        }

        Concat(array: T[]): Linq<T>
        {
            return new Linq<T>(this.a.concat(array));
        }

        Contains(value: T, comparer?: IEqualityComparer<T>): boolean
        {
            if (!comparer)
                return this.Any(o => o === value);

            return this.Any(o => comparer.Equals(o, value));
        }

        Count(selector?: (value: T) => boolean): number
        {
            if (selector)
                return this.Where(selector).Count();

            return this.a.length;
        }

        Distinct(comparer?: IEqualityComparer<T>): Linq<T>
        {
            return this.DistinctBy(o => o, comparer);
        }

        DistinctBy<U>(selector: (e: T) => U, comparer?: IEqualityComparer<T>): Linq<T>
        {
            var a = this.a;
            var e;

            var keys = []
                , unique: T[] = [];

            for (var i = 0, n = a.length; i < n; ++i)
            {
                e = a[i];

                var objKey = selector(e);

                if (!keys.AsLinq().Contains(objKey, comparer))
                {
                    keys.push(objKey);
                    unique.push(e);
                }
            }

            return new Linq<T>(unique);
        }

        ElementAt(index: number): T
        {
            if (index < 0 || index >= this.a.length)
                throw "Index was out of range. Must be non-negative and less than the size of the collection.";

            return this.a[index];
        }

        ElementAtOrDefault(index: number, defaultValue: T): T
        {
            if (index >= this.a.length || index < 0)
                return defaultValue;

            return this.a[index];
        }

        Except(except: T[], comparer?: IEqualityComparer<T>): Linq<T>
        {
            var a = this.a;

            var result: T[] = [];
            var hashTable = {};

            var e, eHash: number;
            var getHash = comparer ? comparer.GetHashCode : e => Object.GetHashCode(e);

            for (var i = 0, n = except.length; i < n; ++i)
            {
                hashTable[getHash(except[i])] = 1;
            }

            for (var i = 0, n = a.length; i < n; ++i)
            {
                e = a[i];
                eHash = getHash(e);

                if (!hashTable[eHash])
                    result.push(e);
            }

            return new Linq<T>(result);
        }

        First(selector?: (e: T) => boolean): T
        {
            if (this.a.length === 0)
                throw "Enumeration does not contain elements";

            if (!selector)
                return this.a[0];

            var result = this.Where(selector);
            if (result.Count() === 0)
                throw "Enumeration does not contain elements";

            return result.ElementAt(0);
        }

        FirstOrDefault(selector?: (e: T) => boolean, defaultValue?: T): T
        {
            if (!selector)
                return this.a.length > 0 ? this.a[0] : defaultValue;

            return this.Where(selector).ElementAtOrDefault(0, defaultValue);
        }

        ForEach(callback: (e: T, index: number) => any): void
        {
            var a = this.a;

            for (var i = 0, n = a.length; i < n; ++i)
            {
                if (callback(a[i], i) === false)
                    break;
            }
        }

        GroupBy<TKey, TElement>(keySelector: (e: T) => TKey
            , elementSelector?: (e: T) => TElement
            , comparer?: IEqualityComparer<TKey>): Linq<any>
        {
            elementSelector = elementSelector || (o => <any>o);
            comparer = comparer || { Equals: (a, b) => a == b, GetHashCode: (e) => Object.GetHashCode(e) };

            var a = this.a;

            var key: TKey, hashKey: number, reHashKey: number;
            var hashs = {};
            for (var i = 0, n = a.length; i < n; ++i)
            {
                reHashKey = undefined;

                key = keySelector(a[i]);
                hashKey = comparer.GetHashCode(key);

                if (typeof hashs[hashKey] !== "undefined")
                    reHashKey = comparer.Equals(key, <TKey>hashs[hashKey].Key) ? hashKey : hashKey + i;

                if (typeof reHashKey !== "undefined" && reHashKey !== hashKey)
                    hashKey = reHashKey;

                hashs[hashKey] = hashs[hashKey] || { Key: key, Elements: [] };
                hashs[hashKey].Elements.push(elementSelector(a[i]));
            }

            var keys = Object.keys(hashs);
            var ret: IGrouping<TKey, T>[] = [];
            for (var i = 0, n = keys.length; i < n; ++i)
            {
                ret.push(hashs[keys[i]]);
            }

            return new Linq<any>(ret);
        }

        IndexOf(e: T, comparer?: IEqualityComparer<T>): number
        {
            var a = this.a;

            if (comparer)
            {
                for (var i = 0, n = a.length; i < n; ++i)
                {
                    var ce = a[i];

                    if (comparer.Equals(ce, e))
                    {
                        return i;
                    }
                }
            }
            else
            {
                for (var i = 0, n = a.length; i < n; ++i)
                {
                    if (a[i] === e)
                    {
                        return i;
                    }
                }
            }

            return -1;
        }

        Intersect(array: T[], comparer?: IEqualityComparer<T>): Linq<T>
        {
            var result: T[] = [];

            for (var i = 0, n = array.length; i < n; ++i)
            {
                if (this.Contains(array[i], comparer))
                {
                    result.push(array[i]);
                }
            }

            return new Linq<T>(result);
        }

        Join<TInner, TKey, TResult>(array: TInner[]
            , outerKeySelector: (e: T) => TKey
            , innerKeySelector: (e: TInner) => TKey
            , resultSelector: (outer: T, inner: TInner) => TResult
            , comparer?: IEqualityComparer<TKey>): Linq<TResult>
        {
            var result: TResult[] = [];

            var outer = this.Select<TKey>(outerKeySelector);
            var inner = array.AsLinq<TInner>().Select<TKey>(innerKeySelector);

            for (var i = 0, n = outer.Count(); i < n; ++i)
            {
                var outerKey = outer.ElementAt(i);

                var index: number = -1;
                if ((index = inner.IndexOf(outerKey, comparer)) != -1)
                {
                    var innerKey = inner.ElementAt(index);

                    result.push(resultSelector(<any>outerKey, <any>innerKey));
                }
            }

            return new Linq<TResult>(result);
        }

        Last(predicate?: (e: T) => boolean): T
        {
            if (this.a.length === 0)
                throw "Enumeration does not contain elements";

            if (!predicate)
                return this.a[this.a.length - 1];

            var result = this.Where(predicate);
            if (result.Count() === 0)
                throw "Enumeration does not contain elements";

            return result.Last();
        }

        LastOrDefault(predicate?: (e: T) => boolean, defaultValue?: T): T
        {
            if (this.a.length === 0)
                return defaultValue;

            if (!predicate)
                return this.a[this.a.length - 1];

            var result = this.Where(predicate);
            if (result.Count() === 0)
                return defaultValue;

            return result.LastOrDefault(null, defaultValue);
        }

        Max<TResult>(selector?: (e: T) => TResult): TResult
        {
            var a = this.a;

            if (a.length === 0)
                throw "Sequence contains no elements.";

            selector = selector || (o => <any>o);

            var max = selector(a[0]);

            for (var i = 0, n = a.length; i < n; ++i)
            {
                var next = selector(a[i]);
                if (next > max)
                    max = next;
            }

            return max;
        }

        Min<TResult>(selector?: (e: T) => TResult): TResult
        {
            var a = this.a;

            if (a.length === 0)
                throw "Sequence contains no elements.";

            selector = selector || (o => <any>o);

            var min = selector(a[0]);

            for (var i = 0, n = a.length; i < n; ++i)
            {
                var next = selector(a[i]);
                if (next < min)
                    min = next;
            }

            return min;
        }

        OrderBy<TKey>(keySelector: (e: T) => TKey, comparer?: (a: TKey, b: TKey) => number): Linq<T>
        {
            comparer = comparer || ((a, b) => <any>a > <any>b ? 1 : -1);

            this.a.sort((l, r) => comparer(keySelector(l), keySelector(r)));
            return this;
        }

        OrderByDescending<TKey>(keySelector: (e: T) => TKey, comparer?: (a: TKey, b: TKey) => number): Linq<T>
        {
            comparer = comparer || ((a, b) => <any>a > <any>b ? 1 : -1);

            comparer = (function (comparer)
            {
                return (a, b) => -comparer(a, b);
            })(comparer);

            this.a.sort((l, r) => comparer(keySelector(l), keySelector(r)));
            return this;
        }

        Reverse(): Linq<T>
        {
            var right = this.a.length - 1;
            for (var left = 0; left < right; ++left, --right)
            {
                var temporary = this.a[left];
                this.a[left] = this.a[right];
                this.a[right] = temporary;
            }

            return this;
        }

        Select<TResult>(selector: (e: T, i?: number) => TResult): Linq<TResult>
        {
            var a = this.a;

            var result: TResult[] = [];

            for (var i = 0, n = a.length; i < n; ++i)
            {
                result.push(selector(a[i]));
            }

            return new Linq<TResult>(result);
        }

        SelectMany<TResult>(selector: (e: T) => T[], resultSelector?: (e: T) => TResult): Linq<TResult>
        {
            var a = this.a;

            var result: T[] = [];

            for (var i = 0, n = a.length; i < n; ++i)
            {
                result = result.concat(selector(a[i]));
            }

            if (!resultSelector)
                return result.AsLinq<TResult>();
            else
                return result.AsLinq<T>().Select<TResult>(resultSelector);
        }

        SequenceEqual(second: T[], comparer?: (a: T, b: T) => boolean): boolean
        {
            var a = this.a;

            if (typeof a === "undefined" || typeof second === "undefined")
            {
                throw "Do not pass null values to arrays.";
            }

            if (a === second)
            {
                return true;
            }

            if (a.length !== second.length)
            {
                return false;
            }

            if (comparer)
            {
                for (var i = 0, n = a.length; i < n; i++)
                {
                    if (!comparer(a[i], second[i]))
                        return false;
                }
            }
            else
            {
                for (var i = 0, n = a.length; i < n; i++)
                {
                    if (a[i] !== second[i])
                        return false;
                }
            }

            return true;
        }

        Single(predicate?: (e: T) => boolean): T
        {
            var a = this.a;

            if (!predicate)
            {
                if (a.length != 1)
                    throw "Source has none or more than one element";

                return a[0];
            }

            var found: T = null;

            for (var i = 0, n = a.length; i < n; ++i)
            {
                if (predicate(a[i]))
                {
                    if (found != null)
                        throw "Source has more than one element";

                    found = a[i];
                }
            }

            return found;
        }

        SingleOrDefault(predicate?: (e: T) => boolean, defaultValue?: T): T
        {
            var a = this.a;

            if (!predicate)
            {
                if (a.length != 1)
                    return defaultValue;

                return a[0];
            }

            var found: T = null;

            for (var i = 0, n = a.length; i < n; ++i)
            {
                if (predicate(a[i]))
                {
                    if (found != null)
                        return defaultValue;

                    found = a[i];
                }
            }

            return found;
        }

        Skip(count: number): Linq<T>
        {
            return new Linq<T>(this.a.slice(count));
        }

        SkipWhile(predicate: (e: T) => boolean): Linq<T>
        {
            var a = this.a
                , i = 0;

            for (var n = a.length; i < n; ++i)
            {
                if (predicate(a[i]) === false)
                    break;
            }

            return new Linq<T>(a.slice(i));
        }

        Sum(selector?: (value: T) => number): number
        {
            var a = this.a;

            var result = 0;

            if (selector)
            {
                for (var i = 0, n = a.length; i < n; ++i)
                {
                    result += selector(a[i]);
                }
            }
            else
            {
                for (var i = 0, n = a.length; i < n; ++i)
                {
                    result += <any>a[i];
                }
            }

            return result;
        }

        Take(count: number): Linq<T>
        {
            var a = this.a;
            var result: T[] = [];

            var len = count > (len = a.length) ? len : count;
            for (var i = 0; i < len; ++i)
            {
                result.push(a[i]);
            }

            return new Linq<T>(result);
        }

        TakeWhile(predicate: (e: T) => boolean): Linq<T>
        {
            var a = this.a;
            var result: T[] = [];

            for (var i = 0, n = a.length, e; i < n; ++i)
            {
                e = a[i];

                if (predicate(e))
                {
                    result.push(e);
                }
                else
                {
                    break;
                }
            }

            return new Linq<T>(result);
        }

        Union(second: T[], comparer?: IEqualityComparer<T>): Linq<T>
        {
            var a = this.a;
            var result: T[] = [];
            var hashTable = {};

            var e, eHash: number;
            var getHash = comparer ? comparer.GetHashCode : e => Object.GetHashCode(e);

            for (var i = 0, n = a.length; i < n; ++i)
            {
                e = a[i];
                eHash = getHash(e);

                if (!hashTable[eHash])
                {
                    hashTable[eHash] = e;
                    result.push(e);
                }
            }

            for (var i = 0, n = second.length; i < n; ++i)
            {
                e = second[i];
                eHash = getHash(e);

                if (!hashTable[eHash])
                {
                    hashTable[eHash] = e;
                    result.push(e);
                }
            }

            return new Linq<T>(result);
        }

        Where(selector: (value: T) => boolean): Linq<T>
        {
            var a = this.a;

            var e;
            var result: T[] = [];

            for (var i = 0, n = a.length; i < n; ++i)
            {
                e = a[i];
                if (selector(e))
                {
                    result.push(e);
                }
            }

            return new Linq(result);
        }

        Zip<TInner, TResult>(array: TInner[], resultSelector: (o: T, i: TInner) => TResult): Linq<TResult>
        {
            var a = this.a;
            var result: TResult[] = [];

            var len = a.length > array.length ? array.length : a.length;

            for (var i = 0, n = len; i < n; ++i)
            {
                result.push(resultSelector(a[i], array[i]));
            }

            return new Linq<TResult>(result);
        }

        ToArray(): T[]
        {
            return this.a;
        }
    }

    export interface IEqualityComparer<T>
    {
        Equals(x: T, y: T): boolean;
        GetHashCode(obj: T): number;
    }

    export interface IGrouping<TKey, T>
    {
        Key: TKey;
        Elements: T[];
    }
}

interface Array<T>
{
    AsLinq<T>(): TSLinq.Linq<T>;
}
Array.prototype.AsLinq = function <T>(): TSLinq.Linq<T>
{
    return new TSLinq.Linq<T>(this);
}

interface Object { GetHashCode(e): number; }
Object.GetHashCode = function (e)
{
    if (e instanceof Number)
        return Number.GetHashCode(e);

    var s: string = e instanceof Object ? JSON.StringifyNonCircular(e) : e.toString();

    var hash = 0;
    if (s.length === 0) return hash;
    for (var i = 0; i < s.length; ++i)
    {
        hash = ((hash << 5) - hash) + s.charCodeAt(i);
    }
    return hash;
};

Number.GetHashCode = function (e) { return e.valueOf(); };

interface Object { IsPlain(e): boolean; }
Object.IsPlain = function (e)
{
    if ((typeof (e) !== "object" || e.nodeType || (e instanceof Window))
        || (e.constructor && !({}).hasOwnProperty.call(e.constructor.prototype, "isPrototypeOf"))
        )
    {
        return false;
    }

    return true;
};

interface JSON { StringifyNonCircular(obj: any): string; }
JSON.StringifyNonCircular = function (obj)
{
    var s = s || "";

    for (var i in obj)
    {
        var o = obj[i];

        if (o && (o instanceof Array || o.IsPlain()))
        {
            s += i + ":" + JSON.stringify(o);
        }
        else if (o && typeof o === "object")
        {
            s += i + ":" + "$ref#" + o;
        }
        else
        {
            s += i + ":" + o;
        }
    }

    return s;
};
